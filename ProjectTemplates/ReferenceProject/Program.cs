using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;

namespace ReferenceProject
{
	public class Program
    {
        public static int  Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();

                return 0;
            }
            catch(Exception ex)
            {
                Log.Logger.Fatal(ex, "An unhandled exception occurred. The application will be closed");
                return 1;
            }
            finally
			{
                Log.CloseAndFlush();
			}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#using-environment-variables-in-configuration-options
                    var envPath = Path.Combine(hostingContext.HostingEnvironment.ContentRootPath, ".env");
                    DotNetEnv.Env.Load(envPath);

                    config.AddEnvironmentVariables();
                })
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                )
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();

                    // https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#logging
                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(context.Configuration)
                        .CreateLogger();

                    logging.AddSerilog(Log.Logger);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
