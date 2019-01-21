using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Serilog;

using Autofac.Extensions.DependencyInjection;

namespace ReferenceProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
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
                .ConfigureAppConfiguration(x => 
                {
                    x.AddEnvironmentVariables();
                })
                .UseStartup<Startup>();
    }
}
