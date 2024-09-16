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

        // PUT: api/DateHours/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDateHours(int id, DateHours dateHours)
        //{
        //    if (id != dateHours.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(dateHours).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DateHoursExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

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

            existingDateHours.FacilityId = dateHours.FacilityId;
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

        //private readonly IDateHoursRepository _repository;

        //public DateHoursController(IDateHoursRepository repository)
        //{
        //    _repository = repository;
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<DateHours>>> GetAll()
        //{
        //    var dateHours = await _repository.GetAllAsync();
        //    return Ok(dateHours);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<DateHours>> GetById(int id)
        //{
        //    var dateHours = await _repository.GetByIdAsync(id);
        //    if (dateHours == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(dateHours);
        //}

        //[HttpPost]
        //public async Task<ActionResult<DateHours>> Create(DateHours dateHours)
        //{
        //    if (dateHours == null)
        //    {
        //        return BadRequest("DateHours object is null.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    await _repository.AddAsync(dateHours);
        //    return CreatedAtAction(nameof(GetById), new { id = dateHours.Id }, dateHours);
        //}


        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, DateHours dateHours)
        //{
        //    if (id != dateHours.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _repository.UpdateAsync(dateHours);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _repository.DeleteAsync(id);
        //    return NoContent();
        //}
    }
}
