using LetsTryMVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var orders = _context.CustomerOrders
                .Where(x => x.Email == User.Identity.Name)
                .ToList();

            ViewBag.User = User.Identity.Name;
            return View(orders);
        }
    }
}
