using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.ViewModels
{
        public class AddProductCategoryViewModel
        {
            [Required(ErrorMessage = "Please enter a new category")]
            [StringLength(100, ErrorMessage = "Category must be below 100 characters.")]
            public string Name { get; set; }

            public AddProductCategoryViewModel() { }
        }
 }
