using System.Net;
using System.Threading.Tasks;
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
            var corsOptions = CorsConfig.ConfigureCors(Settings.AllowedOrigins);
            app.UseCors(corsOptions);

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();

            FormatterConfig.Configure(config);
            RouteConfig.Configure(config);
            LoggerConfig.Configure(config);
            AutofacConfig.Configure(config);
            OptionsMessageHandlerConfig.Configure(config);

            app.UseAutofacMiddleware(AutofacConfig.Container);
            app.UseAutofacWebApi(config);

            /*
            app.Use((ctx, next) =>
            {
                //ctx.Response.Headers.Append("Access-Control-Allow-Origin", ctx.Request.Headers["Origin"] ?? "*");
                //ctx.Response.Headers.Append("Access-Control-Allow-Methods", "*");
                //ctx.Response.Headers.Append("Access-Control-Allow-Headers", "Authorization,Content-Type,If-Modified-Since,Cache-Control");
                //ctx.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");

                //ctx.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
                if (ctx.Request.Method == "OPTIONS")
                {
                    ctx.Response.Headers.Append("Content-Type", "application/json");

                    var apiExplorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();

                    var controllerRequested = ctx.Request.GetRouteData().Values["controller"] as string;
                    var supportedMethods = apiExplorer.ApiDescriptions.Where(d =>
                    {
                        var controller = d.ActionDescriptor.ControllerDescriptor.ControllerName;
                        return string.Equals(
                            controller, controllerRequested, StringComparison.OrdinalIgnoreCase);
                    })
                    .Select(d => d.HttpMethod.Method)
                    .Distinct();

                    if (!supportedMethods.Any())
                        return Task.Factory.StartNew(
                            () => request.CreateResponse(HttpStatusCode.NotFound));

                    return Task.Factory.StartNew(() =>
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.OK);
                        //resp.Headers.Add("Access-Control-Allow-Origin", "*");
                        resp.Headers.Add(
                            "Access-Control-Allow-Methods", string.Join(",", supportedMethods));

                        return resp;
                    });

                    if (!ctx.Request.Headers.ContainsKey("Authorization"))
                    {
                        ctx.Response.Headers.Append("WWW-Authenticate", "Bearer");
                    }
                    ctx.Response.StatusCode = (int)HttpStatusCode.OK;

                    return Task.FromResult(0);
                }

                return next();
            });
            */

            app.UseWebApi(config);
        }
    }
}
