using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using ReferenceProject.Model;

namespace ReferenceProject
{
    public class ODataModel
    {
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<Product>("Products");

            return builder.GetEdmModel();
        }
    }
}