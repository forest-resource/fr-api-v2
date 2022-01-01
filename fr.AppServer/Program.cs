using Autofac.Extensions.DependencyInjection;
using fr.AppServer;
using fr.AppServer.Infrastructor.Host;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostBuilder, config) =>
    {
        config.Sources.Clear();
        config.AddEnvironmentVariables();

        var configuration = config.Build();
        var envName = configuration.GetValue("ENVIRONMENT_NAME", hostBuilder.HostingEnvironment.EnvironmentName);
        
        Log.Information(envName);

        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true);

        if (args.Length > 0)
        {
            config.AddCommandLine(args);
        }
    }).UseSerilog((host, config) => config.ReadFrom.Configuration(host.Configuration))
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseIISIntegration();
        webBuilder.UseStartup<Startup>();
    })
    .Build();

await host.Initialization();

await host.RunAsync();
