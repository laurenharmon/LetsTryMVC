using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsTryMVC.Data;
using LetsTryMVC.Models;
using Microsoft.AspNetCore.Http;

namespace LetsTryMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CartItem> items = _context.CartItems
               .ToList();

            return View(items);
        }

        public void AddToCart(int id)
        {
            var cartItem = _context.CartItems
                .SingleOrDefault(c => c.ProductId == id);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = id,
                    Product = _context.Products.SingleOrDefault(
                    p => p.Id == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };            
                _context.SaveChanges();
            }
            else
            {
                cartItem.Quantity++;
            }
            _context.SaveChanges();
        }

    }
}
