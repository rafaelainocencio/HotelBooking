using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int  Level { get; set; }
        public bool InMaintence { get; set; }

        public bool IsAvaliable()
        {
            if (this.InMaintence || this.HasGuest())
            {
                return false;
            }

            return true;
        }

        public bool HasGuest()
        {
            //verificar se existem bookings abertos para esta room
             return true;
        }
    }
}
