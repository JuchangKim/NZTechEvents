using System;

namespace NZTechEvents.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; } // Add this line to define the primary key
        public DateTime CommentDate { get; set; }
        public string Content { get; set; } = null!;
    }
}