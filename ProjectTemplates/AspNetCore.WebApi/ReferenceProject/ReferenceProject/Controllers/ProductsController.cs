using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using ReferenceProject.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController: ControllerBase
    {
        Repo.IProductsRepo ProductsRepo { get; }
        IMapper Mapper { get; }

        public ProductsController(Repo.IProductsRepo productsRepo, IMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            ProductsRepo = productsRepo ?? throw new ArgumentNullException(nameof(productsRepo));
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <response code="200">List of all of the products</response>
        [HttpGet]
        public IEnumerable<Dto.Product> Get()
        {
            return ProductsRepo.Get().Select(Mapper.Map<Dto.Product>);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <response code="200">Product with the specified id</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id}")]
        public Dto.Product GetById(int id)
        {
            return Mapper.Map<Dto.Product>(ProductsRepo.GetById(id));
        }

        /// <summary>
        /// Example of an exception handling
        /// </summary>
        [HttpGet("ThrowAnException")]
        public IActionResult ThrowAnException()
        {
            throw new Exception("Example exception");
        }
    }
}
