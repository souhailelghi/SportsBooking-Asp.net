

using SportsBookingSystem.Modles;
using SportsBookingSystem.Modles.Dto;

namespace SportsManagementApp.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> RegisterUserAsync(UserDto user);
        Task<User> LoginUserAsync(string email, string password);
    }
}
