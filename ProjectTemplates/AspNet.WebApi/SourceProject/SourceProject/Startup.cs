using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SourceProject.Startup))]

namespace SourceProject
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();

            FormatterConfig.Configure(config);
            RouteConfig.Configure(config);
            LoggerConfig.Configure(config);
            AutofacConfig.Configure(app, config);

            app.UseWebApi(config);
        }
    }
}
