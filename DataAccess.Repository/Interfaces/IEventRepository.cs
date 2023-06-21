using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository.Interfaces
{
    public interface IEventRepository : IDisposable
    {
        IList<Event> GetEventsList();
        Task<Event> GetEvent(int id);
        Task<int> CreateEvent(Event Event);
        Task DeleteEvent(int id);
        int UpdateEvent(Event Event);
        Task Save();
    }
}
