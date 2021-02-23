using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ReferenceProject.Configuration;
using ReferenceProject.Filters;
using ReferenceProject.Modules;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

/*
 * This project have been originally created from ASP.Net Core RESTful Service Template.
 * Getting started guide: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki/Getting-Started-Guide
 * More information about configuring project: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki
 */

namespace ReferenceProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Startup.Configuration = configuration;

            // See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#content-formatting
            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
#if DEBUG
                    Formatting = Formatting.Indented
#else
                    Formatting = Formatting.None
#endif
                };
                settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                return settings;
            };
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors()
                // Add useful interface for accessing the ActionContext outside a controller.
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                // Add useful interface for accessing the HttpContext outside a controller.
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                // Add useful interface for accessing the IUrlHelper outside a controller.
                .AddScoped<IUrlHelper>(x => x
                    .GetRequiredService<IUrlHelperFactory>()
                    .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext))
                .AddMvcCore(options =>
                {
                    options.Filters.Add(new ValidateModelFilter()); // Validate model. See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#model-validation
                    options.Filters.Add(new CacheControlFilter());  // Add "Cache-Control" header. See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#cache-control
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddApiExplorer()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services
                .AddAutoMapper(typeof(Startup)) // Check out Configuration/AutoMapperProfiles/DefaultProfile to do actual configuration. See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#automapper
                .AddSwagger();                  // Check out Configuration/DependenciesConfig.cs/AddSwagger to do actual configuration. See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#documenting-api

            services.AddRouting();
            services.AddControllers();
            services.AddHealthChecks();

            services.AddSettings();
        }

        /// <summary>
        /// Configure Autofac DI-container
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <remarks>
        /// ConfigureContainer is where you can register things directly
        /// with Autofac. This runs after ConfigureServices so the things
        /// here will override registrations made in ConfigureServices.
        /// Don't build the container; that gets done for you.
        /// 
        /// See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#dependency-injection
        /// </remarks>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add things to the Autofac ContainerBuilder.
            builder.RegisterModule<DefaultModule>();
            builder.RegisterModule(new ConfigurationModule(Configuration));
        }

        /// <summary>
        /// Configure Autofac DI-container for production
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <remarks>
        /// This only gets called if your environment is Production. The
        /// default ConfigureContainer won't be automatically called if this
        /// one is called.
        /// 
        /// See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#dependency-injection
        /// </remarks>
        public void ConfigureProductionContainer(ContainerBuilder builder)
        {
            ConfigureContainer(builder);

            // Add things to the ContainerBuilder that are only for the
            // production environment.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            // Use an exception handler middleware before any other handlers
            // See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#unhandled-exceptions-handling
            app.UseExceptionHandler();

            app.UseRouting();

            // See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#cross-origin-resource-sharing-cors-and-preflight-requests
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app
                .UseOptionsVerbHandler()    // Options verb handler must be added after CORS. See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#cross-origin-resource-sharing-cors-and-preflight-requests
                .UseSwaggerWithOptions();   // Check out Configuration/MiddlewareConfig.cs/UseSwaggerWithOptions to do actual configuration. See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#documenting-api

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHealthChecks(Constants.Health.EndPoint); // See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#health-checks
            });

            logger.LogInformation("Server configuration is completed");
        }
    }
}
