using System;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ReferenceProject
{
    /// <summary>
    /// Represents formatter configuration.
    /// </summary>
    public static class FormatterConfig
    {
        /// <summary>
        /// Configures formatter to use JSON and removes XML formatter.
        /// </summary>
        /// <param name="config"></param>
        public static void Configure(HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
#if DEBUG
                Formatting = Formatting.Indented,
#else
                Formatting = Formatting.None,
#endif
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}