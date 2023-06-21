using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces.Services;
using DataAccess.Models;
using DataAccess.Repository;
using DataAccess.Repository.Implementations;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Implementations.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;

        public EventService(IMapper mapper, IEventRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IList<EventPL>> GetEventList()
        {
            return _mapper.Map<IList<EventPL>>(_repository.GetEventsList());
        }

        public async Task<EventPL> GetEvent(int id)
        {
            var Event = await _repository.GetEvent(id);

            if (Event == null)
            {
                throw new Exception("Event doesn't exist");
            }

            return _mapper.Map<EventPL>(Event);
        }

        public async Task<int> CreateEvent(EventPL EventPL)
        {
            var id = await _repository.CreateEvent(_mapper.Map<Event>(EventPL));
            await _repository.Save();
            return id;
        }

        public async Task<int> UpdateEvent(EventPL EventPL)
        {
            var id = _repository.UpdateEvent(_mapper.Map<Event>(EventPL));
            await _repository.Save();
            return id;
        }

        public async Task DeleteEvent(int id)
        {
            await _repository.DeleteEvent(id);
            await _repository.Save();
        }
    }
}
