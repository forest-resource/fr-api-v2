using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fr.AppServer.Infrastructor.Configurations
{
    public static class AuthConfiguration
    {
        private static readonly string AssemblyName = typeof(Startup).Assembly.GetName().Name;

        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("Cookies")
                .AddCookie("Cookies");

            services.AddAuthorization();

            return services;
        }

        public static IApplicationBuilder UseAuthConfiguration(this IApplicationBuilder builder)
        {
            builder.UseAuthentication();
            builder.UseAuthorization();

            return builder;
        }
    }
}