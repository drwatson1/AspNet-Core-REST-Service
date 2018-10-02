using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutofacSerilogIntegration;

namespace ReferenceProject
{
    /// <summary>
    /// Represent Autofac configuration.
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// Configured instance of <see cref="IContainer"/>
        /// <remarks><see cref="AutofacConfig.Configure"/> must be called before trying to get Container instance.</remarks>
        /// </summary>
        protected internal static IContainer Container;

        /// <summary>
        /// Initializes and configures instance of <see cref="IContainer"/>.
        /// </summary>
        /// <param name="config"></param>
        public static void Configure(HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
            builder.RegisterLogger();
            builder.RegisterInstance(AutoMapperConfig.Configure(config));

            RegisterServices(builder);

            Container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }

#pragma warning disable RECS0154 // Parameter is never used
        private static void RegisterServices(ContainerBuilder builder)
#pragma warning restore RECS0154 // Parameter is never used
        {
            // TODO: Register additional services for injection
            // For more information see https://github.com/drwatson1/AspNet-WebApi/wiki#dependency-injection
        }
    }
}