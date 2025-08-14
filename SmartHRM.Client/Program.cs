using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages
builder.Services.AddRazorPages();

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddHttpClient("SmartHRMApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5293/"); // your API port
});

// One registration for Registration & Organization pages
builder.Services.AddHttpClient("MyAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5293/");
});

var app = builder.Build();

// Error handling for production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();
