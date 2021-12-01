using Autofac.Extensions.DependencyInjection;
using fr.AppServer;
using fr.AppServer.Infrastructor.Host;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostBuilder, config) =>
    {
        config.Sources.Clear();

        var env = hostBuilder.HostingEnvironment;

        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);

        config.AddEnvironmentVariables();

        if (args.Length > 0)
        {
            config.AddCommandLine(args);
        }
    }).UseSerilog((host, config) => config.ReadFrom.Configuration(host.Configuration))
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    }).Build();

host.Initialization();

host.RunAsync();
