using System.ComponentModel.DataAnnotations;

namespace SportsBookingSystem.Modles
{
    public class FacilityList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FacilityCode { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        public byte Status { get; set; }

        [Required]
        public byte DeleteFlag { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

       
    }
}
