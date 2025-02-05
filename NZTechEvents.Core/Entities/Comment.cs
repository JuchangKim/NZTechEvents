using System;

namespace NZTechEvents.Core.Entities
{
    public class Comment
    {
        public string id { get; set; }// Primary key
        public DateTime CommentDate { get; set; }
        public string Content { get; set; }
        public string EventId { get; set; }
        public Event Event { get; set; }
        public string UserName { get; set; }
    }
}