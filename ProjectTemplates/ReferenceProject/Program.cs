using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
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
            try
			{
                CreateHostBuilder(args).Build().Run();

                return 0;
            }
            catch(Exception ex)
			{
                var msg = "An unhandled exception occurred. The application will be closed";
                Log.Logger?.Fatal(ex, msg);
                if( Log.Logger == null )
				{
                    Console.WriteLine(msg + Environment.NewLine + ex);
				}
                return 1;
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
