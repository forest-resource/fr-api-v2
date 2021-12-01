using fr.AppServer.Infrastructor.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace fr.AppServer.Infrastructor.Configurations
{
    public static class ControllersConfiguration
    {
        public static IServiceCollection AddControllersConfiguration(this IServiceCollection services)
        {
            services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
                cfg.LowercaseQueryStrings = true;
            });

            services.AddControllers(cfg =>
            {
                //cfg.Filters.Add<IndicatorAuthorizationFilter>();
                cfg.Filters.Add<GlobalActionHandlingFilter>();
                cfg.Filters.Add<GlobalExceptionHandlingFilter>();
            }).AddNewtonsoftJson(cfg =>
            {
                cfg.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                cfg.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                cfg.SerializerSettings.Formatting = Formatting.Indented;
                cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            return services;
        }

        public static IApplicationBuilder UseControllersConfiguration(this IApplicationBuilder builder)
        {
            builder.UseStaticFiles();
            builder.UseRouting();
            builder.UseEndpoints(cfg =>
            {
                cfg.MapControllers();
            });

            return builder;
        }
    }
}
