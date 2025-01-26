using Microsoft.AspNetCore.Mvc;
using NZTechEvents.Core.Entities;
using NZTechEvents.Infrastructure.Data;

namespace NZTechEvents.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventRepository _repo;

        public EventsController(EventRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IAsyncEnumerable<Event> Get()
        {
            return _repo.GetAllEventsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetById(string id)
        {
            var evt = await _repo.GetEventAsync(id);
            if (evt == null) return NotFound();
            return Ok(evt);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> Create([FromBody] Event evt)
        {
            var created = await _repo.CreateEventAsync(evt);
            return CreatedAtAction(nameof(GetById), new { id = created.EventId }, created);
        }
    }
}
