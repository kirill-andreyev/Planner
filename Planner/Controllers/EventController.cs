using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Planner.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            return Ok(await _eventService.GetEventList());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            return Ok(await _eventService.GetEvent(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(string name, string promoter, string description, string speaker,
            string place, DateTime date)
        {
            var Event = new EventPL
            {
                Date = date,
                Description = description,
                Name = name,
                Place = place,
                Promoter = promoter,
                Speaker = speaker
            };
            return Ok(await _eventService.CreateEvent(Event));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(int id, string name, string promoter, string description,
            string speaker, string place, DateTime date)
        {
            var Event = new EventPL
            {
                Date = date,
                Description = description,
                Name = name,
                Place = place,
                Promoter = promoter,
                Speaker = speaker,
                Id = id
            };

            return Ok(await _eventService.UpdateEvent(Event));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEvent(id);
            return Ok();
        }
    }
}
