using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository.Implementations
{
    public class EventRepository : IEventRepository
    {
        private ApplicationDbContext _context;
        private bool disposed = false;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IList<Event> GetEventsList()
        {
            return _context.Events.ToList();
        }

        public async Task<Event> GetEvent(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<int> CreateEvent(Event Event)
        {
            var entity = await _context.Events.AddAsync(Event);
            return entity.Entity.Id;
        }

        public async Task DeleteEvent(int id)
        {
            var dbEvent = await _context.Events.FindAsync(id);
            _context.Events.Remove(dbEvent);
        }

        public int UpdateEvent(Event Event)
        {
           var entity = _context.Events.Update(Event);
           return entity.Entity.Id;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
