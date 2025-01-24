// NZTechEvents.Core/Entities/Event.cs
namespace NZTechEvents.Core.Entities
{
    public class Event
    {
        public string EventId { get; set; } // For NoSQL, string/Guid is common
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }     // (online, offline, both)
        public bool IsFree { get; set; }     // or decimal Fee
        public string Description { get; set; }
        public string Industry { get; set; }
        public string RegistrationLink { get; set; }
        
        // Comments stored in the same NoSQL document
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }

    public class Comment
    {
        public DateTime CommentDate { get; set; }
        public string Content { get; set; }
        
    }
}