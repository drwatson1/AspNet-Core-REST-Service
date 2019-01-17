using Microsoft.Extensions.Configuration;
using System;

namespace ReferenceProject
{
    /// <summary>
    /// Use this class to get an access to options
    /// </summary>
    public static class Settings
    {
        public static string GetConnectionString(string name) => Environment.ExpandEnvironmentVariables(Startup.Configuration.GetConnectionString(name));
        public static string Get(string name) => Environment.ExpandEnvironmentVariables(Startup.Configuration[name]);
        public static string TrimUrl(string url) => url.Trim(' ', '/');
        public static string JoinPath(this string src, string path) => $"{TrimUrl(src)}/{TrimUrl(path)}";

        public static class Services
        {
            public static string CoolService { get; } = TrimUrl(Get("CoolServiceEndpoint"));  // %ORIGIN% will be substituted with the value from an environment variable
            public static string AnotherService { get; } = TrimUrl(Get("AnotherServiceEndpoint"));
        }
    }
}
