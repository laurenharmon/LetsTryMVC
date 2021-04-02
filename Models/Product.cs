using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ProductCategory Category { get; set; }
        public int CategoryId { get; set; }

        public Product() { }

        public Product(string name, string description, int price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        private List<Product> products;
        public List<Product> findAll()
        {
            return this.products;
        }

        public Product find(string id)
        {
            return this.products.Single(p => p.Id.ToString() == id);
        }


        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Id == product.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
