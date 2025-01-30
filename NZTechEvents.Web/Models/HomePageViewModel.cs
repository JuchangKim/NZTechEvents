namespace NZTechEvents.Web.Models
{
    using NZTechEvents.Core.Entities;
    using System.Collections.Generic;

    public class HomePageViewModel
    {
        public string SearchTerm { get; set; }
        public string SortBy { get; set; } // e.g. "date", "location", "isFree"

        public List<Event> UpcomingEvents { get; set; } = new();
        public List<Event> PastEvents { get; set; } = new();
    }
}
