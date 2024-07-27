﻿using Entities = Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public virtual DbSet<Entities.Guest> Guests { get; set; }
        public virtual DbSet<Entities.Room> Rooms { get; set; }
        public virtual DbSet<Entities.Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Data.Guest.GuestConfiguration());
            modelBuilder.ApplyConfiguration(new Data.Room.RoomConfiguration());
        }
    }
}
