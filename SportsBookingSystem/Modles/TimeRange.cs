using System.ComponentModel.DataAnnotations;

namespace SportsBookingSystem.Modles
{
    public class TimeRange
    {
        [Key]
        public int Id { get; set; } 
        public TimeSpan HoursStart { get; set; }
        public TimeSpan HoursEnd { get; set; }
        [Required]
        public int DateHoursId { get; set; } 
       
    }
}
