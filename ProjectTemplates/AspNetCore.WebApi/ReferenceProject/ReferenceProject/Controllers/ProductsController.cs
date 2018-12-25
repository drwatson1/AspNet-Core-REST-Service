using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReferenceProject.Exceptions;
using ReferenceProject.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        Repo.IProductsRepo ProductsRepo { get; }
        IMapper Mapper { get; }

        public ProductsController(Repo.IProductsRepo productsRepo, IMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            ProductsRepo = productsRepo ?? throw new ArgumentNullException(nameof(productsRepo));
        }

        [HttpGet]
        public IEnumerable<Dto.Product> Get()
        {
            return ProductsRepo.Get().Select(Mapper.Map<Dto.Product>);
        }

        [HttpGet]
        [Route("{id}")]
        public Dto.Product GetById(int id)
        {
            return Mapper.Map<Dto.Product>(ProductsRepo.GetById(id));
        }

        [HttpGet("ThrowAnException")]
        public void ThrowAnException()
        {
            throw new Exception("Example exception");
        }
    }
}
