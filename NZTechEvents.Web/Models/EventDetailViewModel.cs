using NZTechEvents.Core.Entities; // Add this using directive

namespace NZTechEvents.Web.Models
{
    public class EventDetailViewModel
    {
        public string id { get; set; } // Primary key
        public string EventId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public bool IsFree { get; set; }
        public string RegistrationLink { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        
        public List<Comment> Comments { get; set; } = new();
    }
}
