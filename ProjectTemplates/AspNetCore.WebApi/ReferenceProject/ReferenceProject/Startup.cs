using System.IO;
using Autofac;
using Autofac.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ReferenceProject.Filters;
using ReferenceProject.Modules;

// TODO: Create and validate configuration in web.config:
// https://www.talkingdotnet.com/how-to-increase-file-upload-size-asp-net-core/
// https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-2.1&tabs=aspnetcore2x#webconfig-file

namespace ReferenceProject
{
    public class Startup
    {
        ILogger<Startup> Logger { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            Logger = logger;
            Configuration = configuration;

            var envPath = Path.Combine(env.ContentRootPath, ".env");
            if (File.Exists(envPath))
            {
                DotNetEnv.Env.Load();
            }

            JsonConvert.DefaultSettings = () => 
                new JsonSerializerSettings()
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
        }

        public IConfiguration Configuration { get; }

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
                    options.Filters.Add(new ValidateModelFilter());
                    options.Filters.Add(new CacheControlFilter());
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
#if DEBUG
                    options.SerializerSettings.Formatting = Formatting.Indented;
#else
                    options.SerializerSettings.Formatting = Formatting.None;
#endif
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add things to the Autofac ContainerBuilder.
            builder.RegisterModule<DefaultModule>();
            builder.RegisterModule(new ConfigurationModule(Configuration));
        }

        // This only gets called if your environment is Production. The
        // default ConfigureContainer won't be automatically called if this
        // one is called.
        public void ConfigureProductionContainer(ContainerBuilder builder)
        {
            ConfigureContainer(builder);

            // Add things to the ContainerBuilder that are only for the
            // production environment.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app/*, IHostingEnvironment env*/)
        {
            // Use our exception handler middleware before any other handlers
            app.UseExceptionHandler();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            // Options verb handler must be added after CORS
            app.UseOptionsVerbHandler();

            app.UseMvcWithDefaultRoute();

            Logger.LogInformation("Server started");
        }
    }
}
