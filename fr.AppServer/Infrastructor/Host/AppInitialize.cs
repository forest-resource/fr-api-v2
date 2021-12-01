using fr.Database.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace fr.AppServer.Infrastructor.Host
{
    public static class AppInitialize
    {
        public static IHost Initialization(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

            return host;
        }

    }
}
