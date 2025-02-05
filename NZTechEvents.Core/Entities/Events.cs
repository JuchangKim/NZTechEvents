// NZTechEvents.Core/Entities/Event.cs
using System.Collections.Generic;

namespace NZTechEvents.Core.Entities
{
    public class Event
    {   public string id { get; set; } = null!;
        public string EventId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public string Type { get; set; } = null!; // "online", "offline", "both"
        public bool IsFree { get; set; } 
        public string Description { get; set; } = null!;
        public string Industry { get; set; } = null!;
        public string RegistrationLink { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty; 
        // A list of user comments:
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }

   
}
