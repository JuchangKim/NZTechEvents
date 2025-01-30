using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
var cosmosKey      = builder.Configuration["Cosmos:Key"];
var cosmosDbName   = builder.Configuration["Cosmos:Database"];
var cosmosContainerName = builder.Configuration["Cosmos:Container"];

var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);
builder.Services.AddSingleton(cosmosClient);
builder.Services.AddSingleton(sp => new EventRepository(cosmosClient, cosmosDbName, cosmosContainerName));

// --------------------------------------------------
// 3. Add Controllers with Views (MVC) & Razor
// --------------------------------------------------
builder.Services.AddControllersWithViews();
// or builder.Services.AddRazorPages(); if you prefer Razor Pages
// builder.Services.AddControllers(); if you want Web API only

// Build the app
var app = builder.Build();

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
