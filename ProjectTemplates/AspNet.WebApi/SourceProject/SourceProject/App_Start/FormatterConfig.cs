using System;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace SourceProject
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

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}