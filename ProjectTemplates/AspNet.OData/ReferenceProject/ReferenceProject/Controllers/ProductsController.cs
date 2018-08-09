using Microsoft.AspNet.OData;
using ReferenceProject.Model;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceProject.Controllers
{
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
        public IQueryable<Product> Get()
        {
            return products.AsQueryable();
        }
    }
}