using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZTechEvents.Core.Entities;
using NZTechEvents.Infrastructure.Data;

namespace NZTechEvents.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly NZTechEventsDbContext _context;

        public UsersController(NZTechEventsDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            // In production, hash the password properly
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "User registered", UserId = user.UserId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Simplified check
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return Unauthorized("User not found.");

            // Compare password with user.PasswordHash here
            bool isPasswordValid = user.PasswordHash == password; 
            if (!isPasswordValid) return Unauthorized("Invalid password.");

            return Ok(new { Message = $"Welcome {user.Name}!" });
        }
    }
}
