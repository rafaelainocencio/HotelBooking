﻿using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest
{ 
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        public GuestRepository(HotelDbContext hotelDbContext) 
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> Create(Domain.Entities.Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest.Id;
        }

        public Task<Domain.Entities.Guest> Get(int id)
        {
            return _hotelDbContext.Guests.FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
