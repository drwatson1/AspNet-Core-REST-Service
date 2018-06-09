using Swashbuckle.Application;
using System;
using System.Web.Http;

namespace ReferenceProject
{
    public static class SwaggerConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config
                .EnableSwagger(c => { c.PrettyPrint(); c.SingleApiVersion("v1", "A title for your API"); })
                .EnableSwaggerUi();
        }
    }
}