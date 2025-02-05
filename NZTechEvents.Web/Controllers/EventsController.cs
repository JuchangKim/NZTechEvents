using Microsoft.AspNetCore.Mvc;
using NZTechEvents.Core.Entities;
using NZTechEvents.Infrastructure.Data;
using NZTechEvents.Web.Models;
using System.Threading.Tasks;
using System.Linq;

namespace NZTechEvents.Web.Controllers
{
    // No [ApiController]
    // No [Route("api/[controller]")]
    // We'll use conventional routing (e.g. in Program.cs: app.MapControllerRoute(...))

    public class EventsController : Controller
    {
        private readonly EventRepository _repo;

        public EventsController(EventRepository repo)
        {
            _repo = repo;
        }

        // GET: /Events
        public async Task<IActionResult> Index()
        {
            // If you want a listing in MVC
            var eventsList = await _repo.GetAllEventsAsync().ToListAsync();
            return View(eventsList); // Renders Views/Events/Index.cshtml
        }

        // GET: /Events/Details/{id}
        [HttpGet("Events/Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Event ID required");

            var evt = await _repo.GetEventAsync(id);
            if (evt == null)
                return NotFound();

            // Convert to a ViewModel if you prefer or pass the entity directly
            var vm = new EventDetailViewModel
            {
                EventId = evt.EventId,
                Title = evt.Title,
                Date = evt.Date,
                Location = evt.Location,
                IsFree = evt.IsFree,
                RegistrationLink = evt.RegistrationLink,
                Description = evt.Description,
                ImageUrl = evt.ImageUrl,
                Comments = evt.Comments
            };

            return View(vm); // Renders Views/Events/Details.cshtml
        }

        // (Optional) If you want a create form, etc.
        public IActionResult Create()
        {
            return View(); // Renders a form in Views/Events/Create.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event evt)
        {
            if (!ModelState.IsValid) return View(evt);

            // Create the event in Cosmos
            var created = await _repo.CreateEventAsync(evt);
            // Redirect to details or index
            return RedirectToAction("Details", new { id = created.id });
        }

        // Add more actions for update, delete, comment CRUD, etc.
    }
}
