using AutoMapper;
using ReferenceProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceProject.Repo
{
    public class ProductsRepo: IProductsRepo
    {
        readonly List<Product> products = new List<Product>()
        {
            new Product(1, "lime"),
            new Product(2, "carrot")
        };

        IMapper Mapper { get; }

        public ProductsRepo(IMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IQueryable<Product> Get()
        {
            return products.Select(Mapper.Map<Product>).AsQueryable();
        }

        public void Create(Product p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            if (products.Any(x => x.Id == p.Id))
            {
                throw new DuplicateKeyException($"Can't create object of type {nameof(Product)} with the key '{p.Id}'. Object with the same key is already exists");
            }

            products.Add(Mapper.Map<Product>(p));
        }

        public void Delete(int id)
        {
            var p = products.FirstOrDefault(x => x.Id == id);
            if (p == null)
            {
                throw new KeyNotFoundException($"Object of type '{nameof(Product)}' with the key '{id}' not found");
            }

            products.RemoveAll(x => x.Id == p.Id);
        }

        public void Update(Product p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            var stored = products.FirstOrDefault(x => x.Id == p.Id);
            if (stored == null)
            {
                throw new KeyNotFoundException($"Object of type '{nameof(Product)}' with the key '{p.Id}' not found");
            }

            products.RemoveAll(x => x.Id == stored.Id);
            products.Add(Mapper.Map<Product>(p));
        }
    }
}
