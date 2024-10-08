using DomainEntities = Domain.Booking.Entities;

namespace Domain.Booking.Ports
{
    public interface IBookingRepositoy
    {
        Task<Entities.Booking> Get(int id);
        Task<int> Create(Entities.Booking booking);
    }
}
