using LetsTryMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.ViewModels
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description for your product")]
        [StringLength(500, ErrorMessage = "Description must be below 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public int Price { get; set; }
        public DateTime LastUpdated { get; set; }

        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public AddProductViewModel(List<ProductCategory> categories)
        {
            Categories = new List<SelectListItem>();

            foreach (var category in categories)
            {
                Categories.Add(
                    new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name
                    });
            }
        }

        public AddProductViewModel() { }
    }
}
