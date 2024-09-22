using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBookingSystem.Modles
{
    public class BookingList
    {
      
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int SportId { get; set; }

        public DateTime BookingTime { get; set; }

        [Required]
        public TimeSpan DateFrom { get; set; }

        [Required]
        public TimeSpan DateTo { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; }

        [ForeignKey(nameof(UserId))]
        public List<Guid> UserIdList { get; set; }

    }
}


//{
//    "id": 0,
//  "userId": "cfc0c3be-9b4d-4d30-bac3-2af6615a3c5a",
//  "sportId": 1,
//  "bookingTime": "2024-09-21T21:07:01.770Z",
//  "dateFrom":"02:00:00",
//  "dateTo": "03:00:00",
//  "dateCreated": "2024-09-21T21:07:01.770Z",
//  "dateUpdated": "2024-09-21T21:07:01.770Z",
//  "userIdList": [
//    "13433437-b79b-402d-95f0-4fbafa29e124"
//  ]
//}
