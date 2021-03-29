using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public Photo(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        public Photo() { }

    }
}
