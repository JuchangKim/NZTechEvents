using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using NZTechEvents.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------
// 1. Configure EF Core for SQL Server (Users)
// --------------------------------------------------
builder.Services.AddDbContext<NZTechEventsDbContext>(options =>
{
    // A typical connection string from appsettings.json
    // e.g. "Server=...;Database=...;User Id=...;Password=...;"
    var connString = builder.Configuration.GetConnectionString("SqlServerConnection");
    options.UseSqlServer(connString);
});

// --------------------------------------------------
// 2. Configure CosmosClient for Events
// --------------------------------------------------
var cosmosEndpoint = builder.Configuration["Cosmos:Endpoint"];
var cosmosKey = builder.Configuration["Cosmos:Key"];
var cosmosDbName = builder.Configuration["Cosmos:Database"];
var cosmosContainerName = builder.Configuration["Cosmos:Container"];

var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);
builder.Services.AddSingleton(cosmosClient);
builder.Services.AddSingleton<EventRepository>(sp =>
    new EventRepository(cosmosClient, cosmosDbName, cosmosContainerName));

// --------------------------------------------------
// 3. Add Controllers
// --------------------------------------------------
builder.Services.AddControllers();

// Build the app
var app = builder.Build();

// Optional: app.UseHttpsRedirection();
app.MapControllers();

app.Run();
