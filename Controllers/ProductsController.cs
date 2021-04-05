using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LetsTryMVC.Data;
using LetsTryMVC.Models;
using LetsTryMVC.ViewModels;

namespace LetsTryMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public IActionResult Index()
        {
            List<Product> prdcts = _context.Products
               .Include(x => x.Category)
               .ToList();

            return View(prdcts);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            List<ProductCategory> categories = _context.Categories.ToList();
            AddProductViewModel addProductViewModel = new AddProductViewModel(categories);
            return View(addProductViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddProductViewModel addProductViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductCategory category = _context.Categories.Find(addProductViewModel.CategoryId);
                Product newProduct = new Product
                {
                    Name = addProductViewModel.Name,
                    Description = addProductViewModel.Description,
                    Price = addProductViewModel.Price,
                    Category = category,
                    LastUpdated = addProductViewModel.LastUpdated
                };

                _context.Products.Add(newProduct);
                _context.SaveChanges();

                return Redirect("/products");
            }

            return RedirectToAction("Create");
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            Product editProduct = _context.Products.Find(id);
            ViewBag.Title = "Edit: " + editProduct.ToString();

            return View();
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Price,CategoryId,Category,LastUpdated")] Product product)
        {
            Product editProduct = _context.Products.Find(id);
            ProductCategory category = _context.Categories.Find(product.CategoryId);

            editProduct.Name = product.Name;
            editProduct.Description = product.Description;
            editProduct.Price = product.Price;
            editProduct.CategoryId = product.CategoryId;
            //editProduct.Category = product.Category;
            editProduct.LastUpdated = product.LastUpdated;

            _context.SaveChanges();

            return Redirect("/Products");

        }

        // GET: Products/Delete/5
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);

            _context.SaveChanges();

            return Redirect("/Products");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
