using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NZTechEvents.Infrastructure.Data
{
    public class NZTechEventsDbContextFactory : IDesignTimeDbContextFactory<NZTechEventsDbContext>
    {
        public NZTechEventsDbContext CreateDbContext(string[] args)
        {
            // Adjust the path to the appsettings.json file
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NZTechEvents.Web"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<NZTechEventsDbContext>();
            var connectionString = configuration.GetConnectionString("SqlServerConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new NZTechEventsDbContext(optionsBuilder.Options);
        }
    }
}