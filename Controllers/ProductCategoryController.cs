using LetsTryMVC.Data;
using LetsTryMVC.Models;
using LetsTryMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Controllers
{
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext context;

        public ProductCategoryController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.title = "All Categories";

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Add Product Category";
            AddProductCategoryViewModel addProductCategoryViewModel = new AddProductCategoryViewModel();
            return View();
        }

        [HttpPost, Route("/productcategory/create")]
        public IActionResult Create(AddProductCategoryViewModel addProductCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductCategory newCategory = new ProductCategory
                {
                    Name = addProductCategoryViewModel.Name
                };

                context.Categories.Add(newCategory);
                context.SaveChanges();

                return Redirect("/productcategory");
            }
            return View();
        }
    }
}