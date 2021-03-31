using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ContentType { get; set; }
        public byte[] Image { get; set; }
        public FileType FileType { get; set; }
        public ProductCategory Category { get; set; }
        public int CategoryId { get; set; }

        public Photo(string imageName, byte[] image)
        {
            ImageName = imageName;
            Image = image;
        }

        public Photo() { }

    }
}
