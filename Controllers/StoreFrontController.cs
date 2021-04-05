using LetsTryMVC.Data;
using LetsTryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Controllers
{
    public class StoreFrontController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreFrontController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return Content("Item not found.");
            }

            ProductCategory category = _context.Categories.Find(id);
            if (category == null)
            {
                return Content("Item not found.");
            }
            return View(category);
        }
    }
}
