using Microsoft.EntityFrameworkCore;

using SportsManagementApp.Interfaces;

using BCrypt.Net;
using SportsBookingSystem.Data;
using SportsBookingSystem.Modles;
using SportsBookingSystem.Modles.Dto;

namespace SportsManagementApp.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }



        public async Task<User> RegisterUserAsync(UserDto userDto)
        {
            // Check if the email already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userDto.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already in use.");
            }

            // Map UserDto to User entity
            var user = new User
            {
                UserId = Guid.NewGuid(), // Or generate appropriately
                Name = userDto.Name,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password) // Hash the password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<User> LoginUserAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null; // Authentication failed
            }

            return user; // Successful authentication
        }

      
    }
}
