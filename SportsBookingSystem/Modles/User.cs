﻿using System.ComponentModel.DataAnnotations;

namespace SportsBookingSystem.Modles
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
