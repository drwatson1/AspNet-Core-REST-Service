using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using SerilogWeb.Classic;
using System.IO;
using System.Reflection;
using System.Web.Http;
using static System.Environment;

namespace SourceProject
{
    public static class LoggerConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            // Use Seriog for logging
            // More information can be found here https://github.com/serilog/serilog/wiki/Getting-Started

            var f = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var name = f.Name.Replace(f.Extension, "");

            // TODO: Adjust log file location and name. 
            // By default log file is located in 'C:\Users\<username>\AppData\Roaming\Logs' folder and named as the current assembly name
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(ExpandEnvironmentVariables($"%AppData%/Logs/{name}..txt"), rollingInterval: RollingInterval.Day)
                .ReadFrom.AppSettings()

                // Enrich with SerilogWeb.Classic (https://github.com/serilog-web/classic)
                .Enrich.WithHttpRequestUrl()
                .Enrich.WithHttpRequestType()

                .Enrich.WithExceptionDetails()

                .CreateLogger();

            // By defaut we don't want to see all HTTP requests in log file, but you can change this by ajusting this setting
            // Additional information can be found here https://github.com/serilog-web/classic
            // TODO: Change WebApi requests logging level
            SerilogWebClassic.Configure(cfg => cfg
              .LogAtLevel(LogEventLevel.Debug));  // All requests will
        }
    }
}
