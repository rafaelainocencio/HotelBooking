using DomainEntities =  Domain.Guest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Guest.Ports
{
    public interface IGuestRepository
    {
        Task<DomainEntities.Guest> Get(int id);
        Task<int> Create(DomainEntities.Guest guest);
    }
}
