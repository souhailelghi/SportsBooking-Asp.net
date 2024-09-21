using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsBookingSystem.Data;
using SportsBookingSystem.IRepositorys;
using SportsBookingSystem.Modles;

namespace SportsBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateHoursController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DateHoursController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/DateHours/GetAllDateHoursBySportId/{sportId}
        [HttpGet("GetAllDateHoursBySportId/{sportId}")]
        public async Task<ActionResult<IEnumerable<DateHours>>> GetAllDateHoursBySportId(int sportId)
        {
            var dateHoursList = await _context.DateHours
                .Include(d => d.TimeRanges)
                .Where(d => d.SportId == sportId) // Filtering by SportId
                .ToListAsync();

            if (dateHoursList == null || !dateHoursList.Any())
            {
                return NotFound();
            }

            return Ok(dateHoursList);
        }


        // GET: api/DateHours/GetAllDateHoursById/{id}
        [HttpGet("GetAllDateHoursById/{id}")]
        public async Task<ActionResult<IEnumerable<DateHours>>> GetAllDateHoursById(int id)
        {
            var dateHoursList = await _context.DateHours
                .Include(d => d.TimeRanges)
                .Where(d => d.SportId == id) // Assuming SportId or any other related ID field
                .ToListAsync();

            if (dateHoursList == null || !dateHoursList.Any())
            {
                return NotFound();
            }

            return Ok(dateHoursList);
        }


        // GET: api/DateHours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DateHours>>> GetDateHours()
        {
            return await _context.DateHours.Include(d => d.TimeRanges).ToListAsync();
        }

        // GET: api/DateHours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DateHours>> GetDateHours(int id)
        {
            var dateHours = await _context.DateHours
                .Include(d => d.TimeRanges)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dateHours == null)
            {
                return NotFound();
            }

            return dateHours;
        }

        // POST: api/DateHours
        [HttpPost]
        public async Task<IActionResult> PostDateHours(DateHours dateHours)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DateHours.Add(dateHours);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostDateHours), new { id = dateHours.Id }, dateHours);
        }

      

        // DELETE: api/DateHours/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDateHours(int id, DateHours dateHours)
        {
            if (id != dateHours.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDateHours = await _context.DateHours
                .Include(d => d.TimeRanges)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (existingDateHours == null)
            {
                return NotFound();
            }

            existingDateHours.SportId = dateHours.SportId;
            existingDateHours.Day = dateHours.Day;
            existingDateHours.DateCreation = dateHours.DateCreation;

            // Remove existing time ranges
            _context.TimeRanges.RemoveRange(existingDateHours.TimeRanges);

            // Add new time ranges
            foreach (var timeRange in dateHours.TimeRanges)
            {
                existingDateHours.TimeRanges.Add(timeRange);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDateHours(int id)
        {
            var dateHours = await _context.DateHours.FindAsync(id);
            if (dateHours == null)
            {
                return NotFound();
            }

            _context.DateHours.Remove(dateHours);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DateHoursExists(int id)
        {
            return _context.DateHours.Any(e => e.Id == id);
        }

        // GET: api/DateHours/day/Monday
        [HttpGet("day/{day}")]
        public async Task<ActionResult<IEnumerable<DateHours>>> GetDateHoursByDay(string day)
        {
            var dateHoursList = await _context.DateHours
                .Include(d => d.TimeRanges)
                .Where(d => d.Day.ToLower() == day.ToLower()) // Case-insensitive comparison
                .ToListAsync();

            if (!dateHoursList.Any())
            {
                return NotFound();
            }

            return dateHoursList;
        }


      
    }
}
