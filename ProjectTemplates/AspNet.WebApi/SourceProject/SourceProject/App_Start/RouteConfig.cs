using System.Web.Http;

namespace SourceProject
{
    public static class RouteConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            /*
            using (var handler = new RedirectHandler(m => m.RequestUri.ToString(), "swagger"))
            {
                config.Routes.MapHttpRoute(
                    name: "swagger_root",
                    routeTemplate: "",
                    defaults: null,
                    constraints: null,
                    handler: handler);
            }*/
        }
    }
}
