using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBookingSystem.Modles
{
    public class DateHours
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FacilityId { get; set; }
        public string Day { get; set; }

        
        public ICollection<TimeRange> TimeRanges { get; set; } = new List<TimeRange>();

        public DateTime DateCreation { get; set; } = DateTime.Now;
    }
}
