using LetsTryMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.ViewModels
{
    public class AddPhotoViewModel
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Title { get; set; }
        public string ImageName { get; set; }

        [Required(ErrorMessage ="Please choose a photo.")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage ="You must choose a category")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public AddPhotoViewModel(List<ProductCategory> categories)
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

        public AddPhotoViewModel() { }
    }
}
