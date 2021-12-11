using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fr.AppServer.Infrastructor.Configurations
{
    public static class AuthConfiguration
    {
        private static readonly string AssemblyName = typeof(Startup).Assembly.GetName().Name;
        private static readonly string DefaultSchema = JwtBearerDefaults.AuthenticationScheme;
        private static readonly string CookieSchema = CookieAuthenticationDefaults.AuthenticationScheme;

        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(DefaultSchema)
                .AddJwtBearer(DefaultSchema, options => configuration.Bind("JwtSettings", options))
                .AddCookie(CookieSchema, options => configuration.Bind("CookieSettings", options))
                .AddGoogle("Google", options =>
                {
                    options.ClientId = configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                });

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