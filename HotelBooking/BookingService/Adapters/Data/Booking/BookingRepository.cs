using Domain.Booking.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepositoy
    {
        public readonly HotelDbContext _hotelDbContext;
        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> Create(Domain.Booking.Entities.Booking booking)
        {
            _hotelDbContext.Bookings.Add(booking);
            await _hotelDbContext.SaveChangesAsync();
            return booking.Id;
        }

        public Task<Domain.Booking.Entities.Booking> Get(int id)
        {
            return _hotelDbContext.Bookings.Include(x => x.Room)
                                           .Include(x => x.Guest)
                                           .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
