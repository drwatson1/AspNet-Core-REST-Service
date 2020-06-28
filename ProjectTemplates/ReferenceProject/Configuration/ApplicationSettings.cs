using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Contrib.Extensions.Configuration;

namespace ReferenceProject.Configuration
{
    public static class ApplicationSettings
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddOptions<Settings.Products>()
                .Bind(Configuration.GetSection(Settings.Products.SectionName))
                .SubstituteVariables();
        }
    }
}
