using System.ComponentModel.DataAnnotations;

namespace SportsBookingSystem.Modles
{
    public class CategoryList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public byte DeleteFlag { get; set; }

        [Required]
        public byte Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; }
    }
}
