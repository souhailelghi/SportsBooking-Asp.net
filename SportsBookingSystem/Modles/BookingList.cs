using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBookingSystem.Modles
{
    public class BookingList
    {
        [Key]
        public int Id { get; set; }

 

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int SportId { get; set; }

        [Required]
        public TimeSpan DateFrom { get; set; }

        [Required]
        public TimeSpan DateTo { get; set; }
        

       

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; }

    }
}
