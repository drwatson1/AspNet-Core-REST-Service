using System;
using System.Configuration;

namespace SourceProject
{
    /// <summary>
    /// Represents all application settings
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Get value from appSettings section of web.config and expand environmemt variables
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns></returns>
        public static string Get(string name) => Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings[name] ?? "");
        public static string GetConnectionString(string name) => Environment.ExpandEnvironmentVariables(ConfigurationManager.ConnectionStrings[name]?.ConnectionString ?? "");

        public static string AllowedOrigins => Get(Constants.Settings.AllowedOrigins);
    }
}
