using SmartHRM.Infrastructure.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace SmartHRM.Infrastructure.Services;

public static class DataSeeder
{
    public static async Task EnsureMigratedAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<SmartHRMDbContext>();
        await db.Database.EnsureCreatedAsync(); // or MigrateAsync in prod
    }
}
