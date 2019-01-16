using System;

namespace ReferenceProject.Model
{
    public class Product: IEntity
    {
        public Product()
        {
        }

        public Product(int id)
        {
            Id = id;
        }

        public Product(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
