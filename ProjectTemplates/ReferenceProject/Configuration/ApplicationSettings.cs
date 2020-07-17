using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Contrib.Extensions.Configuration;

namespace ReferenceProject.Configuration
{
    public static class ApplicationSettings
    {
        public static void AddSettings(this IServiceCollection services)
        {
            services.AddOptions<Settings.Products>()
                .AutoBind()
                .SubstituteVariables();
        }
    }
}
