using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Services.Interfaces.Services
{
    public interface IEventService
    {
        public Task<IList<EventPL>> GetEventList();
        public Task<EventPL> GetEvent(int id);
        public Task<int> CreateEvent(EventPL Event);
        public Task<int> UpdateEvent(EventPL Event);
        public Task DeleteEvent(int id);
    }
}
