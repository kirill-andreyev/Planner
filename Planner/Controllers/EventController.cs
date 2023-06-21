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
            try
            {
                return Ok(await _eventService.GetEventList());
            }
            catch 
            {
                return NotFound();
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            try
            {
                return Ok(await _eventService.GetEvent(id));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventPL Event)
        {
            try
            {
                Event.Id = await _eventService.CreateEvent(Event);
            }
            catch 
            {
                return Problem();
            }
            return Created($"{Event.Id}", Event);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(EventPL Event)
        {
            try
            {
                Event.Id = await _eventService.UpdateEvent(Event);
            }
            catch
            {
                return Problem();
            }

            return Created($"{Event.Id}", Event);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                await _eventService.DeleteEvent(id);
            }
            catch
            {
                return Problem();
            }
            return Ok();
        }
    }
}
