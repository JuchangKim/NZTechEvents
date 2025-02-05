using Microsoft.AspNetCore.Mvc;
using NZTechEvents.Core.Entities;
using NZTechEvents.Infrastructure.Data;
using NZTechEvents.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NZTechEvents.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventRepository _eventRepository;

        // Instead of injecting IHttpClientFactory, inject your EventRepository
        public HomeController(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IActionResult> Index(string search, string sort = "date")
        {
            // Retrieve the events directly from Cosmos (or EF) via your repository
            var allEvents = await _eventRepository.GetAllEventsAsync().ToListAsync();

            // Filter by search
            if (!string.IsNullOrWhiteSpace(search))
            {
                allEvents = allEvents
                    .Where(e => e.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Sort
            switch (sort.ToLower())
            {
                case "location":
                    allEvents = allEvents.OrderBy(e => e.Location).ToList();
                    break;
                case "isfree":
                    allEvents = allEvents.OrderBy(e => e.IsFree).ToList();
                    break;
                default: // "date"
                    allEvents = allEvents.OrderBy(e => e.Date).ToList();
                    break;
            }

            // Separate upcoming vs. past
            var now = DateTime.UtcNow;
            var upcomingEvents = allEvents.Where(e => e.Date >= now).ToList();
            var pastEvents = allEvents.Where(e => e.Date < now).ToList();

            var vm = new HomePageViewModel
            {
                SearchTerm = search,
                SortBy = sort,
                UpcomingEvents = upcomingEvents,
                PastEvents = pastEvents
            };

            return View(vm);
        }
    }
}
