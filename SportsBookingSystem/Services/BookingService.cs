using Microsoft.EntityFrameworkCore;
using SportsBookingSystem.Data;

using SportsBookingSystem.Modles;

namespace SportsBookingSystem.Services
{
    public class BookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CanTeamOrUserBookAsync(Guid userId, List<Guid> userIdList, int sportId)
        {
            var sport = await _context.Sports.FindAsync(sportId);
            if (sport == null)
            {
                return false; // Sport not found
            }

            var delayTimeMinutes = sport.DelayTime; // Assuming DelayTime is a property of sport
            var delayTime = DateTime.UtcNow.AddMinutes(-delayTimeMinutes);

            var existingBooking = await _context.Bookings
                .Where(b => b.UserId == userId && b.DateCreated >= delayTime)
                .FirstOrDefaultAsync();

            if (existingBooking != null)
            {
                return false; // User has a booking within the delay time
            }

            var usersExist = await _context.Users
                .Where(u => userIdList.Contains(u.UserId))
                .Select(u => u.UserId)
                .ToListAsync();

            if (usersExist.Count != userIdList.Count)
            {
                return false; // Some users from the list don't exist in the database
            }

            var teamBookingExists = await _context.Bookings
                .Where(b => userIdList.Contains(b.UserId) && b.DateCreated >= delayTime)
                .AnyAsync();

            return !teamBookingExists; // Return true if no team members have bookings within the delay time
        }

        public async Task<bool> BookAsync(Guid userId,DateTime bookingTime, TimeSpan HoursStart, TimeSpan HoursEnd, List<Guid> userIdList, int sportId)
        {
            

            if (!await CanTeamOrUserBookAsync(userId, userIdList, sportId))
            {
                return false;
            }

           

          
            var booking = new BookingList
            {
                UserId = userId,
                SportId = sportId,
                BookingTime = bookingTime,
                DateFrom = HoursStart,
                DateTo = HoursEnd, 
                DateCreated = DateTime.UtcNow ,
                UserIdList = userIdList ?? new List<Guid>()
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<BookingList> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

    }
}
