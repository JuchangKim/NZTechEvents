// NZTechEvents.Core/Entities/Event.cs
namespace NZTechEvents.Core.Entities
{
    public class Event
    {
        public string EventId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public string Type { get; set; } = null!; // "online", "offline", "both"
        public bool IsFree { get; set; } 
        public string Description { get; set; } = null!;
        public string Industry { get; set; } = null!;
        public string RegistrationLink { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        // A list of user comments:
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }

    public class Comment
    {
        public DateTime CommentDate { get; set; } = DateTime.UtcNow;
        public string Content { get; set; } = null!;
    }
}
