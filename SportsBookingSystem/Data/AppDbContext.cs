//using Microsoft.EntityFrameworkCore;
//using SportsBookingSystem.Modles;

//namespace SportsBookingSystem.Data
//{
//    public class AppDbContext : DbContext
//    {
//        public AppDbContext(DbContextOptions<AppDbContext> options)
//        : base(options)
//        {
//        }

//        public DbSet<BookingList> Bookings { get; set; }
//        public DbSet<CategoryList> Categories { get; set; }
//        public DbSet<ClientList> Clients { get; set; }
//        public DbSet<sportList> Sports { get; set; }
//        public DbSet<SystemInfo> SystemInfos { get; set; }
//        public DbSet<DateHours> DateHours { get; set; }
//        public DbSet<TimeRange> TimeRanges { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<TimeRange>(entity =>
//            {
//                entity.HasNoKey(); // Configure TimeRange as keyless entity
//                entity.ToView(null); // Ensure it is not mapped to a database table
//            });

//            // Example: Configure the default values and constraints
//            modelBuilder.Entity<BookingList>()
//                .Property(b => b.Status)
//                .HasDefaultValue(0);

//            modelBuilder.Entity<CategoryList>()
//                .Property(c => c.Status)
//                .HasDefaultValue(1);

//            modelBuilder.Entity<ClientList>()
//                .Property(c => c.Status)
//                .HasDefaultValue(1);

//            modelBuilder.Entity<sportList>()
//                .Property(f => f.Status)
//                .HasDefaultValue(1);
//        }




//    }
//}


using Microsoft.EntityFrameworkCore;

using SportsBookingSystem.Modles;

namespace SportsBookingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<BookingList> Bookings { get; set; }
        public DbSet<CategoryList> Categories { get; set; }
        public DbSet<ClientList> Clients { get; set; }
        public DbSet<SportList> Sports { get; set; }
        public DbSet<SystemInfo> SystemInfos { get; set; }
        public DbSet<DateHours> DateHours { get; set; }
        public DbSet<TimeRange> TimeRanges { get; set; } // Add this if TimeRange is a standalone entity
        public DbSet<User> Users { get; set; }

        public DbSet<Personne> Personnes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            //modelBuilder.Entity<TimeRange>()
            //    .HasOne(tr => tr.DateHours)
            //    .WithMany(dh => dh.TimeRanges)
            //    .HasForeignKey(tr => tr.DateHoursId)
            //    .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed

            // Example: Configure the default values and constraints
            //modelBuilder.Entity<BookingList>()
            //    .Property(b => b.Status)
            //    .HasDefaultValue(0);

            //modelBuilder.Entity<CategoryList>()
            //    .Property(c => c.Status)
            //    .HasDefaultValue(1);

            //modelBuilder.Entity<ClientList>()
            //    .Property(c => c.Status)
            //    .HasDefaultValue(1);

            //modelBuilder.Entity<sportList>()
            //    .Property(f => f.Status)
            //    .HasDefaultValue(1);
        }
    }
}
