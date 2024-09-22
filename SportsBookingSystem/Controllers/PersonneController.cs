using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsBookingSystem.Data;
using SportsBookingSystem.Modles;

namespace SportsBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonneController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonneController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Personne
        [HttpPost]
        public async Task<IActionResult> PostPersonne([FromForm] PersonneCreateDto personneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            byte[] imageData = null;
            if (personneDto.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    await personneDto.Image.CopyToAsync(ms);
                    imageData = ms.ToArray(); // Convert the uploaded image to a byte array
                }
            }

            var personne = new Personne
            {
                Name = personneDto.Name,
                Image = imageData // Store the image as byte[] in the database
            };

            _context.Personnes.Add(personne);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonne), new { id = personne.Id }, personne);
        }

        // Helper method to retrieve a Personne
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonne(int id)
        {
            var personne = await _context.Personnes.FindAsync(id);

            if (personne == null)
            {
                return NotFound();
            }

            return Ok(personne);
        }

        // GET: api/Personne
        [HttpGet]
        public async Task<IActionResult> GetAllPersonnes()
        {
            var personnes = await _context.Personnes.ToListAsync();
            return Ok(personnes);
        }
    }
}
