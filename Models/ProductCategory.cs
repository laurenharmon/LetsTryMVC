using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

        public ProductCategory(string name)
        {
            Name = name;
        }

        public ProductCategory() { }
    }
}
