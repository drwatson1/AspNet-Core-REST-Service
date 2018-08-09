using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using ReferenceProject.Model;
using System.Web.Http;

namespace ReferenceProject
{
    public static class ODataConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            // OData configuration
            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.MapODataServiceRoute("ODataRoute", null, ODataModel.GetEdmModel());
        }
    }
}
