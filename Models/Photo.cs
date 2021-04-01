using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public ProductCategory Category { get; set; }
        public int CategoryId { get; set; }

    }
}
