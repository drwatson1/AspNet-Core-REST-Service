using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SourceProject
{
    public static class Constants
    {
        public static class Settings
        {
            public static string AllowedOrigins => "allowed-origins";

            public static class Auth
            {
                public static string Issuer => "oauth2:issuer";
                public static string Audience => "oauth2:audience";
                public static string CertThumbprint => "oauth2:cert-thumbprint";
            }
        }
    }
}
