using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsBookingSystem.Data;
using SportsBookingSystem.Modles;

namespace SportsBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemInfoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SystemInfoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemInfo>>> GetSystemInfos()
        {
            return await _context.SystemInfos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SystemInfo>> GetSystemInfo(int id)
        {
            var systemInfo = await _context.SystemInfos.FindAsync(id);
            if (systemInfo == null)
            {
                return NotFound();
            }
            return systemInfo;
        }

        [HttpPost]
        public async Task<ActionResult<SystemInfo>> PostSystemInfo(SystemInfo systemInfo)
        {
            _context.SystemInfos.Add(systemInfo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSystemInfo), new { id = systemInfo.Id }, systemInfo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSystemInfo(int id, SystemInfo systemInfo)
        {
            if (id != systemInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(systemInfo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemInfoExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSystemInfo(int id)
        {
            var systemInfo = await _context.SystemInfos.FindAsync(id);
            if (systemInfo == null)
            {
                return NotFound();
            }

            _context.SystemInfos.Remove(systemInfo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool SystemInfoExists(int id)
        {
            return _context.SystemInfos.Any(e => e.Id == id);
        }
    }
}
