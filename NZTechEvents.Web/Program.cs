//Program.cs

using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NZTechEvents.Infrastructure.Data;
using NZTechEvents.Core.Entities;
using Microsoft.Azure.Cosmos.Linq;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("NZTechEventsAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5000/"); // Set the base address for the API
});

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
var cosmosKey      = builder.Configuration["Cosmos:Key"];
var cosmosDbName   = builder.Configuration["Cosmos:Database"] ?? throw new ArgumentNullException("Cosmos:Database");
var cosmosContainerName = builder.Configuration["Cosmos:Container"] ?? throw new ArgumentNullException("Cosmos:Container");

var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);
builder.Services.AddSingleton(cosmosClient);
builder.Services.AddSingleton(sp => new EventRepository(cosmosClient, cosmosDbName, cosmosContainerName));

// Set up Blobstorage for images
var blobConnectionString = builder.Configuration.GetConnectionString("BlobStorage") ?? throw new ArgumentNullException("BlobStorage");
builder.Services.AddSingleton(new BlobServiceClient(blobConnectionString));

// Build the app
var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<NZTechEventsDbContext>();
    var eventRepository = services.GetRequiredService<EventRepository>();

    try
    {
        // Ensure the database is created
        dbContext.Database.EnsureCreated();

        // Seed data into both databases
        await SeedData.Initialize(dbContext, eventRepository);
    }
    catch (Exception ex)
    {
        // Log the exception (you can use a logging framework here)
        Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // In .NET 8, you still typically use HSTS in production
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// --------------------------------------------------
// 5. Map routes
// --------------------------------------------------
// For a typical MVC route (Home/Index as default):
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// If you have attribute-routed controllers, you could also do:
// app.MapControllers(); 

// Run the app
app.Run();
