using DomainEntities =  Domain.Guest.Entities;

namespace Domain.Guest.Ports
{
    public interface IGuestRepository
    {
        Task<DomainEntities.Guest> Get(int id);
        Task<int> Create(DomainEntities.Guest guest);
    }
}
