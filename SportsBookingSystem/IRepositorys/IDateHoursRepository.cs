using SportsBookingSystem.Modles;

namespace SportsBookingSystem.IRepositorys
{
    public interface IDateHoursRepository
    {
        Task<IEnumerable<DateHours>> GetAllAsync();
        Task<DateHours> GetByIdAsync(int id);
        Task AddAsync(DateHours dateHours);
        Task UpdateAsync(DateHours dateHours);
        Task DeleteAsync(int id);
    }
}
