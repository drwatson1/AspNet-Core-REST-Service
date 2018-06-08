using Microsoft.Owin.Cors;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Cors;

namespace ReferenceProject
{
    /// <summary>
    /// Represents CORS configuration.
    /// </summary>
    public static class CorsConfig
    {
        /// <summary>
        /// Initializes and configures <see cref="CorsOptions"/> instance.
        /// </summary>
        /// <param name="origins">String of allowed origins delimited by: ';'</param>
        public static CorsOptions ConfigureCors(string origins = null)
        {
            if (string.IsNullOrWhiteSpace(origins))
                return CorsOptions.AllowAll;

            var corsPolicy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
                SupportsCredentials = true,
                PreflightMaxAge = 86400
            };

            corsPolicy.Headers.Add("Authorization");

            // StringSplitOptions.RemoveEmptyEntries doesn't remove whitespaces.
            origins.Split(';')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList()
                .ForEach(origin => corsPolicy.Origins.Add(origin));

            if (!corsPolicy.Origins.Any())
                return CorsOptions.AllowAll;

            return new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(corsPolicy)
                }
            };
        }
    }
}