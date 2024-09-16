using System.ComponentModel.DataAnnotations;

namespace SportsBookingSystem.Modles
{
    public class SystemInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MetaField { get; set; }

        [Required]
        public string MetaValue { get; set; }
    }
}
