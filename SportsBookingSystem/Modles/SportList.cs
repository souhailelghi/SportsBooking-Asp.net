using System.ComponentModel.DataAnnotations;

namespace SportsBookingSystem.Modles
{
    public class SportList
    {
        [Key]
        public int Id { get; set; }

        [Required]
  
        public int sportCode { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string? ImagePath { get; set; }

        public int NumberPlayer { get; set; }
        
        public int DelayTime { get; set; }
        public string? Condition { get; set; }
       


        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }


       
    }
}
