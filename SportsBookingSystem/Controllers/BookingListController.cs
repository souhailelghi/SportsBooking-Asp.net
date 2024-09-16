using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsBookingSystem.Data;
using SportsBookingSystem.Modles;

namespace SportsBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingListController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingListController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingList>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }
        [HttpGet("dates")]
public async Task<ActionResult<IEnumerable<BookingList>>> GetBookingDates()
{
    var bookingDates = await _context.Bookings
        .Select(b => new
        {
            b.DateFrom,
            b.DateTo
        })
        .ToListAsync();

    return Ok(bookingDates);
}

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingList>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return booking;
        }

        [HttpPost]
        public async Task<ActionResult<BookingList>> PostBooking(BookingList booking)
        {
            // Check if the facility is available
            var isAvailable = await IsFacilityAvailableAsync(
                booking.FacilityId,
                booking.DateFrom,
                booking.DateTo);

            if (!isAvailable)
            {
                return BadRequest(new { status = "failed", msg = "Facility is not available on the selected dates." });
            }

            // Check if a booking with the same DateCreated, DateFrom, and DateTo already exists
            var exists = await _context.Bookings
                .AnyAsync(b => b.FacilityId == booking.FacilityId &&
                               b.DateCreated.Date == booking.DateCreated.Date &&
                               b.DateFrom == booking.DateFrom &&
                               b.DateTo == booking.DateTo &&
                               b.Status == 1); // Assuming 1 is the status for confirmed bookings

            if (exists)
            {
                return BadRequest(new { status = "failed", msg = "A booking with the same details already exists." });
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, BookingList booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllBookings()
        {
            _context.Bookings.RemoveRange(_context.Bookings);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }

        private async Task<bool> IsFacilityAvailableAsync(int facilityId, TimeSpan dateFrom, TimeSpan dateTo)
        {
            var now = DateTime.Now;
            var bookings = await _context.Bookings
                .Where(b => b.FacilityId == facilityId &&
                            ((b.DateFrom < dateTo && b.DateTo > dateFrom) ||
                             (b.DateFrom < dateFrom && b.DateTo > dateTo)) &&
                            b.Status == 1) // Assuming 1 is the status for confirmed bookings
                .ToListAsync();

            return !bookings.Any();
        }
    }
}
