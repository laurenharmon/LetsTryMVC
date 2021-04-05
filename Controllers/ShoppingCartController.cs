using LetsTryMVC.Data;
using LetsTryMVC.Models;
using LetsTryMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LetsTryMVC.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            var addedProduct = _context.Products.Single(product => product.Id == id);

            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string productName = _context.Carts.FirstOrDefault(item => item.ProductId == id).Product.Name;

            int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

     }
}
