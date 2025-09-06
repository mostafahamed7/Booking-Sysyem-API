using Domain.Entites;
using Domain.Entites.Reservations_Mod;
using Microsoft.EntityFrameworkCore;
using Presistance.Data.Configrations;
using System.Reflection;

namespace Presistance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ReservationRoomConfiguration());
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Hotels
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { ID = 1, Name = "Cairo Grand Hotel", Address = "Cairo, Egypt", Stars = 5 },
                new Hotel { ID = 2, Name = "Alexandria Beach Resort", Address = "Alexandria, Egypt", Stars = 4 },
                new Hotel { ID = 3, Name = "Luxor Nile View", Address = "Luxor, Egypt", Stars = 5 },
                new Hotel { ID = 4, Name = "Giza Pyramids Inn", Address = "Giza, Egypt", Stars = 3 },
                new Hotel { ID = 5, Name = "Sharm El Sheikh Paradise", Address = "Sharm El Sheikh, Egypt", Stars = 5 }
                );

            // Rooms
            modelBuilder.Entity<Room>().HasData(
                new Room { ID = 1, NumOfBed = 2, MaxOccupancy = 2, Price = 150, HotelId = 1, IsDeleted = false },
                new Room { ID = 2, NumOfBed = 3, MaxOccupancy = 3, Price = 100, HotelId = 1, IsDeleted = false },
                new Room { ID = 3, NumOfBed = 2, MaxOccupancy = 2, Price = 200, HotelId = 2, IsDeleted = false },
                new Room { ID = 4, NumOfBed = 4, MaxOccupancy = 4, Price = 250, HotelId = 2, IsDeleted = false },
                new Room { ID = 5, NumOfBed = 2, MaxOccupancy = 4, Price = 300, HotelId = 3, IsDeleted = false },
                new Room { ID = 6, NumOfBed = 2, MaxOccupancy = 4, Price = 120, HotelId = 3, IsDeleted = false },
                new Room { ID = 7, NumOfBed = 2, MaxOccupancy = 2, Price = 180, HotelId = 4, IsDeleted = false },
                new Room { ID = 8, NumOfBed = 2, MaxOccupancy = 2, Price = 400, HotelId = 5, IsDeleted = false },
                new Room { ID = 9, NumOfBed = 2, MaxOccupancy = 2, Price = 90, HotelId = 5, IsDeleted = false }
                );
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<RoomPictures> RoomPictures { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; }
    }
}
