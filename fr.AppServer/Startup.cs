using Autofac;
using Autofac.Extensions.DependencyInjection;
using fr.AppServer.Infrastructor.Configurations;
using fr.Core.Timing;
using fr.Database;
using fr.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace fr.AppServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            if (Configuration.GetValue("IsUseLocalTiming", false))
            {
                Clock.Provider = new LocalClockProvider();
            }
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsConfiguration(Configuration);
            services.AddGzipCompression();
            services.AddHealthChecks();

            services.AddAutoMapper(option =>
            {

            });

            services.AddAuthConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.AddControllersConfiguration();
            services.AddMvc(cfg =>
            {
                cfg.EnableEndpointRouting = false;
            });
        }

        public void ConfigurationBuilder(ContainerBuilder builder)
        {
            builder.RegisterModule<InitDbModule>();
            builder.RegisterModule<InitServiceModule>();

            builder.RegisterInstance(Configuration).SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerConfiguration();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCorsConfiguration();
            app.UseGzipCompression();
            app.UseHttpsRedirection();
            app.UseHealthChecks("/health");
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            app.UseAuthConfiguration();
            app.UseControllersConfiguration();
            app.UseMvcWithDefaultRoute();
        }
    }
}
