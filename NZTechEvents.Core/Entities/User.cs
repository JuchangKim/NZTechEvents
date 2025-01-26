// NZTechEvents.Core/Entities/User.cs
namespace NZTechEvents.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
