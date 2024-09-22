using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsBookingSystem.Data;

using SportsBookingSystem.Modles;
using SportsBookingSystem.Services;

namespace SportsBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingListController : ControllerBase
    {
        private readonly BookingService _bookingService;
        private readonly AppDbContext _context;

        public BookingListController(BookingService bookingService, AppDbContext context)
        {
            _bookingService = bookingService;
            _context = context;
        }

        [HttpPost("AddBookings")]
        public async Task<ActionResult<BookingList>> PostBookings(BookingList booking)
        {
            if (booking.UserIdList == null || booking.UserIdList.Count == 0)
            {
                return BadRequest(new { error = "UserIdList cannot be empty." });
            }

            if (booking.SportId <= 0)
            {
                return BadRequest(new { error = "Invalid sport ID." });
            }

            if (await _bookingService.BookAsync(booking.UserId, booking.BookingTime, booking.DateFrom, booking.DateTo, new List<Guid> { booking.UserId }, booking.SportId))
            {
                return Ok(new { message = "Booking successful!" });
            }

            return BadRequest(new { error = "You or your team can't make another booking until the required time has passed since the last booking." });
        }


        [HttpGet("newGet{id}")]
        public async Task<ActionResult<BookingList>> GetNewBooking(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return booking;
        }

        // Get all bookings
        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<IEnumerable<BookingList>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        // Get booking dates (start and end)
        [HttpGet("GetBookingDates")]
        public async Task<ActionResult<IEnumerable<object>>> GetBookingDates()
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

        // Get a specific booking by ID
        [HttpGet("GetBookingBy{id}")]
        public async Task<ActionResult<BookingList>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return booking;
        }

     

        // Update an existing booking
        [HttpPut("UpdateBooking{id}")]
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
                return NotFound();
            }
            return NoContent();
        }

        // Delete a booking by ID
        [HttpDelete("DeleteBooking{id}")]
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

        // Delete all bookings
        [HttpDelete("DeleteAllBookings")]
        public async Task<IActionResult> DeleteAllBookings()
        {
            _context.Bookings.RemoveRange(_context.Bookings);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
