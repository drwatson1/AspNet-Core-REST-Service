using System.IO;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ReferenceProject.Startup))]

namespace ReferenceProject
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Use DotNetEnv v1.1.0 due to it is the only version with out dependencies
            var envFilePath = System.IO.Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, ".env");
            if (File.Exists(envFilePath))
            {
                DotNetEnv.Env.Load(envFilePath);
            }

            var corsOptions = CorsConfig.ConfigureCors(Settings.AllowedOrigins);
            app.UseCors(corsOptions);

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();

            config.Filters.Add(new ExceptionFilter());

            AutoMapperConfig.Configure(); // This call should be made before AutofacConfig.Configure
            AutofacConfig.Configure(config);

            FormatterConfig.Configure(config);
            RouteConfig.Configure(config);
            LoggerConfig.Configure(config);
            OptionsMessageHandlerConfig.Configure(config);
            SwaggerConfig.Configure(config);

            app.UseAutofacMiddleware(AutofacConfig.Container);
            app.UseAutofacWebApi(config);

            app.PreventResponseCaching();

            /*
            TODO: Uncomment this and set up parameters in web.config to enable authentication
            app.UseJsonWebToken(
                issuer: Settings.Auth.Issuer,
                audience: Settings.Auth.Audience,
                signingKey: Settings.Auth.IssuerCertificate
                );
            */

            app.UseWebApi(config);
        }
    }
}
