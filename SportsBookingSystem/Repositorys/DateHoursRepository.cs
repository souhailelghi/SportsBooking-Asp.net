using Microsoft.EntityFrameworkCore;
using SportsBookingSystem.Data;
using SportsBookingSystem.IRepositorys;
using SportsBookingSystem.Modles;

namespace SportsBookingSystem.Repositorys
{
    public class DateHoursRepository : IDateHoursRepository
    {
        private readonly AppDbContext _context;

        public DateHoursRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DateHours>> GetAllAsync()
        {
            return await _context.DateHours.ToListAsync();
        }

        public async Task<DateHours> GetByIdAsync(int id)
        {
            return await _context.DateHours.FindAsync(id);
        }

        public async Task AddAsync(DateHours dateHours)
        {
            _context.DateHours.Add(dateHours);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DateHours dateHours)
        {
            _context.DateHours.Update(dateHours);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dateHours = await _context.DateHours.FindAsync(id);
            if (dateHours != null)
            {
                _context.DateHours.Remove(dateHours);
                await _context.SaveChangesAsync();
            }
        }
    }
}
