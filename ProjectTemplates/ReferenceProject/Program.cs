using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace ReferenceProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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

                    /*
                     * You can use a global logger as this, but I don't recommend this way
                     * More information: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#logging
                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(context.Configuration)
                        .CreateLogger();
                    */

                    logging.AddSerilog(new LoggerConfiguration()
                        .ReadFrom.Configuration(context.Configuration)
                        .CreateLogger());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
