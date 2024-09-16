using System.ComponentModel.DataAnnotations;

namespace SportsBookingSystem.Modles
{
    public class ClientList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        public byte Status { get; set; }

        [Required]
        public byte DeleteFlag { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateAdded { get; set; }
    }
}
