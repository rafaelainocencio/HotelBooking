using Domain.Guest.Ports;
using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;
using RoomEntity = Domain.Room.Entities;

namespace Data.Room
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        public RoomRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> Create(RoomEntity.Room room)
        {
            _hotelDbContext.Rooms.Add(room);
            await _hotelDbContext.SaveChangesAsync();
            return room.Id;
        }

        public Task<RoomEntity.Room> Get(int Id)
        {
            return _hotelDbContext.Rooms
                .Where(g => g.Id == Id).FirstOrDefaultAsync();
        }

        public Task<RoomEntity.Room> GetAggregate(int Id)
        {
            return _hotelDbContext.Rooms
                .Include(r => r.Bookings)
                .Where(g => g.Id == Id).FirstAsync();
        }
    }
}