using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsBookingSystem.Data;
using SportsBookingSystem.Modles;

namespace SportsBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityListController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacilityListController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacilityList>>> GetFacilities()
        {
            return await _context.Facilities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FacilityList>> GetFacility(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            return facility;
        }

        [HttpPost]
        public async Task<ActionResult<FacilityList>> PostFacility(FacilityList facility)
        {
            _context.Facilities.Add(facility);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFacility), new { id = facility.Id }, facility);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacility(int id, FacilityList facility)
        {
            if (id != facility.Id)
            {
                return BadRequest();
            }

            _context.Entry(facility).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacilityExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }

            _context.Facilities.Remove(facility);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool FacilityExists(int id)
        {
            return _context.Facilities.Any(e => e.Id == id);
        }
    }
}
