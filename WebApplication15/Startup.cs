using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using Serilog;
using static System.Environment;

[assembly: OwinStartup(typeof(WebApplication15.Startup))]

namespace WebApplication15
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();
            ConfigureAutofac(app, config);
            ConfigureLogger(app, config);

            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }

        private void ConfigureLogger(IAppBuilder app, HttpConfiguration config)
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
                .CreateLogger();

            Log.Information("aaa");
            Log.Debug("bbb");
        }

        private void ConfigureAutofac(IAppBuilder app, HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
            RegisterDiServices(builder);

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
        }

        private void RegisterDiServices(ContainerBuilder builder)
        {
            // TODO: Place your code here
        }
    }
}
