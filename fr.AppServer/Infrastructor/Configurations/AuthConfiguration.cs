using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace fr.AppServer.Infrastructor.Configurations
{
    public static class AuthConfiguration
    {
        private static readonly string AssemblyName = typeof(Startup).Assembly.GetName().Name;
        private static readonly string DefaultSchema = JwtBearerDefaults.AuthenticationScheme;
        private static readonly string CookieSchema = CookieAuthenticationDefaults.AuthenticationScheme;

        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtSettings:Key"]));

            services.AddAuthentication(DefaultSchema)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = signinKey,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateActor = false,

                        NameClaimType = "name"
                    };
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