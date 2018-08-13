using Microsoft.AspNet.OData;
using ReferenceProject.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;

namespace ReferenceProject.Controllers
{
    // TODO: Use Authorize attribute to claim all actions be authorized
    // [Authorize]
    public class ProductsController : ODataController
    {
        private List<Product> products = new List<Product>()
        {
            new Product()
            {
                ID = 1,
                Name = "Bread",
            },
            new Product()
            {
                ID = 2,
                Name = "Butter",
            }
        };

        [EnableQuery]
        // TODO: Use ScopeAuthorize attribute if you want to allow this actions only to concrete scope
        // [ScopeAuthorize(Constants.Scope.Read)]
        public IQueryable<Product> Get()
        {
            return products.AsQueryable();
        }

        // TODO: Use ScopeAuthorize attribute if you want to allow this actions only to concrete scope
        // [ScopeAuthorize(Constants.Scope.Read)]
        public Product Get(int key)
        {
            var r = (from p in products where p.ID == key select p).FirstOrDefault();
            if( r == null )
            {
                // This will generate 404 Not Found OData-compliant answer as it handled in Handlers\ExceptionFilter
                throw new KeyNotFoundException($"Product with ID='{key}' not found");
            }

            return r;
        }

        // TODO: Use ScopeAuthorize attribute if you want to allow this actions only to concrete scope
        // [ScopeAuthorize(Constants.Scope.Update)]
        public void Post(Product p)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            // TODO: Of cause, you should add the check whether p.ID is unique or not and some other stuff
            products.Add(p);
        }

        // TODO: Add PATCH, PUT and DELETE actions
    }
}
