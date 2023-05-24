using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces.Services;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Implementations.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EventService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IList<EventPL>> GetEventList()
        {
            return _context.Events.Select(_mapper.Map<EventPL>).ToList();
        }

        public async Task<EventPL> GetEvent(int id)
        {
            var Event = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);

            if (Event == null)
            {
                throw new Exception("Event doesn't exist");
            }

            var EventPL = _mapper.Map<EventPL>(Event);
            return EventPL;
        }

        public async Task<int> CreateEvent(EventPL EventPL)
        {
            var entity = await _context.Events.AddAsync(_mapper.Map<Event>(EventPL));
            await _context.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public async Task<int> UpdateEvent(EventPL EventPL)
        {
            var dbEvent = await _context.Events.FirstOrDefaultAsync(x => x.Id == EventPL.Id);
            if (dbEvent == null)
            {
                throw new Exception("Event doesn't exist");
            }

            dbEvent.Date = EventPL.Date;
            dbEvent.Description = EventPL.Description;
            dbEvent.Name = EventPL.Name;
            dbEvent.Place = EventPL.Place;
            dbEvent.Promoter = EventPL.Promoter;
            dbEvent.Speaker = EventPL.Speaker;

            await _context.SaveChangesAsync();

            return dbEvent.Id;
        }

        public async Task DeleteEvent(int id)
        {
            var dbEvent = _context.Events.FirstOrDefault(x => x.Id == id);
            if (dbEvent == null)
            {
                throw new Exception("Event doesn't exist");
            }

            _context.Events.Remove(dbEvent);
            await _context.SaveChangesAsync();
        }
    }
}
