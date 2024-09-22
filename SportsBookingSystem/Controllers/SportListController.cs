using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsBookingSystem.Data;
using SportsBookingSystem.Modles;
using SportsBookingSystem.Modles.Dto;
using System.ComponentModel.DataAnnotations;

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
        public async Task<ActionResult<SportList>> Postsport([FromForm] SportListDto sportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            byte[] imageData = null;
            if (sportDto.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    await sportDto.Image.CopyToAsync(ms);
                    imageData = ms.ToArray(); // Convert the uploaded image to a byte array
                }
            }
        


        var sportList = new SportList
            {
            sportCode = sportDto.sportCode,
            CategoryId = sportDto.CategoryId,
            Image = imageData, // Store the image as byte[] in the database
            NumberPlayer = sportDto.NumberPlayer,
            DelayTime = sportDto.DelayTime,
            Condition = sportDto.Condition,
            DateCreated = sportDto.DateCreated,
            DateUpdated = sportDto.DateUpdated,
            Name = sportDto.Name,
            Description = sportDto.Description

        };

            _context.Sports.Add(sportList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Getsport), new { id = sportList.Id }, sportList);

          
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
