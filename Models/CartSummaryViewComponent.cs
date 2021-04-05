using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private IViewComponentResult Invoke()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return View(cart);
        }
    }
}

