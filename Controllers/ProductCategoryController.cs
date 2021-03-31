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
        private ApplicationDbContext _context;

        public ProductCategoryController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ProductCategory> categories = _context.Categories
               .ToList();

            return View(categories);
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

                _context.Categories.Add(newCategory);
                _context.SaveChanges();

                return Redirect("/productcategory");
            }
            return View();
        }
    }
}