namespace SportsBookingSystem.Modles
{
    public class Personne
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public byte[] Image { get; set; }
    }

    public class PersonneCreateDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; } // The image file uploaded from form
    }
}
