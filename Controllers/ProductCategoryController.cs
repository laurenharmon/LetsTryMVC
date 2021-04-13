using LetsTryMVC.Data;
using LetsTryMVC.Models;
using LetsTryMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Controllers
{
    [Authorize]
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

       
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.Categories
                .First(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}