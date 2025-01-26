// NZTechEvents.Infrastructure/Data/NZTechEventsDbContext.cs
using Microsoft.EntityFrameworkCore;
using NZTechEvents.Core.Entities;

namespace NZTechEvents.Infrastructure.Data
{
    public class NZTechEventsDbContext : DbContext
    {
        public NZTechEventsDbContext(DbContextOptions<NZTechEventsDbContext> options)
            : base(options) 
        {
        }

        public DbSet<User> Users => Set<User>();

        // OnModelCreating if needed for custom configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional EF configurations or constraints if needed
        }
    }
}
