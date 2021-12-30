using fr.Database.EntityFramework;
using fr.Service.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace fr.AppServer.Infrastructor.Host;

public static class AppInitialize
{
    public static async Task<IHost> Initialization(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var authService = scope.ServiceProvider.GetRequiredService<IAccountService>();

        await authService.SeedAllRoles();

        if (configuration.GetValue<bool>("Administrator:HasAdminAccount"))
        {
            var adminUsername = configuration["Administrator:Username"];
            var adminPassword = configuration["Administrator:Password"];

            await authService.SeedAdminAccount(adminUsername, adminPassword);
        }

        return host;
    }

}
