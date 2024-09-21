using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsBookingSystem.Data;
using SportsBookingSystem.Modles;

namespace SportsBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportListController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SportListController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportList>>> GetSports()
        {
            return await _context.Sports.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SportList>> Getsport(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }
            return sport;
        }

        [HttpPost]
        public async Task<ActionResult<SportList>> Postsport(SportList sport)
        {
            _context.Sports.Add(sport);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Getsport), new { id = sport.Id }, sport);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Putsport(int id, SportList sport)
        {
            if (id != sport.Id)
            {
                return BadRequest();
            }

            _context.Entry(sport).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sportExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletesport(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            _context.Sports.Remove(sport);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool sportExists(int id)
        {
            return _context.Sports.Any(e => e.Id == id);
        }
    }
}
