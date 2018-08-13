namespace ReferenceProject
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

        public static class Scope
        {
            // TODO: Adjust scopes as you need
            public const string Read = "odata-service.read";
            public const string Update = "odata-service.update";
        }
    }
}
