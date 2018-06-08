using IdentityModel;
using Serilog;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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

        public static class Auth
        {

            public static X509Certificate2 IssuerCertificate
            {
                get
                {
                    if (signingCertificate != null)
                    {
                        return signingCertificate;
                    }
                    signingCertificate = X509.LocalMachine.My.Thumbprint.Find(IssuerCertThumbprint).FirstOrDefault();
                    if (signingCertificate == null)
                    {
                        Log.Error("Can't find certificate with thumbpring '{cert}'", IssuerCertThumbprint);
                        throw new Exception($"Can't find certificate with thumbpring '{IssuerCertThumbprint}'");
                    }
                    return signingCertificate;
                }
            }

            public static string Issuer => Get(Constants.Settings.Auth.Issuer);
            public static string Audience => Get(Constants.Settings.Auth.Audience);
            public static string IssuerCertThumbprint => Get(Constants.Settings.Auth.CertThumbprint);

            private static X509Certificate2 signingCertificate;
        }
    }
}
