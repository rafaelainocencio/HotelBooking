using Entities = Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }
        
        public virtual DbSet<Entities.Guest.Entities.Guest> Guests { get; set; }
        public virtual DbSet<Entities.Room.Entities.Room> Rooms { get; set; }
        public virtual DbSet<Entities.Guest.Entities.Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Data.Guest.GuestConfiguration());
            modelBuilder.ApplyConfiguration(new Data.Room.RoomConfiguration());
        }
    }
}
