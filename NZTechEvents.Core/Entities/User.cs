// NZTechEvents.Core/Entities/User.cs
namespace NZTechEvents.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }         // or Guid
        public string Email { get; set; }
        public string PasswordHash { get; set; } 
        public string Name { get; set; }
    }
}