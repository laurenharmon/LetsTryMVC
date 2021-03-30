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
        public string Image { get; set; }

        public Photo(string imageName, string image)
        {
            ImageName = imageName;
            Image = image;
        }

        public Photo() { }

    }
}
