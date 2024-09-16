using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBookingSystem.Modles
{
    public class BookingList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string RefCode { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int FacilityId { get; set; }

        [Required]
        public TimeSpan DateFrom { get; set; }

        [Required]
        public TimeSpan DateTo { get; set; }

        [Required]
        public byte Status { get; set; } // 0 = Pending, 1 = Confirmed, 2 = Done, 3 = Cancelled

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; }

    }
}
