using LetsTryMVC.Data;
using LetsTryMVC.Models;
using LetsTryMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ShoppingCart cart = GetCart(this.HttpContext);

            ShoppingCartViewModel viewModel = new ShoppingCartViewModel
            {
                CartItems = GetCartItems(cart),
                CartTotal = GetTotal()
            };

            return View(viewModel);

        }


        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var addedProduct = _context.Products.Single(product => product.Id == id);

            var cart = GetCart(this.HttpContext);

            AddToCart(addedProduct);

            return RedirectToAction("Index");
        }

        public void AddToCart(Product product)
        {
            var Cart = GetCart(this.HttpContext);
            var cartItem = _context.Carts.SingleOrDefault(c => c.CartId == Cart.ShoppingCartId && c.ProductId == product.Id);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductId = product.Id,
                    CartId = Cart.ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _context.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

            _context.SaveChanges();
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = GetCart(this.HttpContext);

            string productName = _context.Carts.FirstOrDefault(item => item.ProductId == id).Product.Name;

            int itemCount = RemoveFromCartMethod(id);

            var results = new ShoppingCartRemoveViewModel
            {
                CartTotal = GetTotal(),
                CartCount = GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        public int RemoveFromCartMethod(int id)
        {
            var Cart = GetCart(this.HttpContext);
            var cartItem = _context.Carts.SingleOrDefault(cart => cart.CartId == Cart.ShoppingCartId && cart.ProductId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _context.Carts.Remove(cartItem);
                }

                _context.SaveChanges();
            }
            return itemCount;
        }

        //public void EmptyCart()
        //{
        //    var Cart = GetCart();
        //    var cartItems = _context.Carts.Where(cart => cart.CartId == Cart.ShoppingCartId);

        //    foreach (var cartItem in cartItems)
        //    {
        //        _context.Carts.Remove(cartItem);
        //    }
        //    _context.SaveChanges();
        //}


        public  ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();

            cart.ShoppingCartId = GetCartId(context);

            return cart;
        }

        public ShoppingCart GetCart()
        {
            return GetCart(this.HttpContext);
        }


        public List<Cart> GetCartItems(ShoppingCart cart)
        {
            var getCart = GetCart();
            string shoppingCartId = cart.ShoppingCartId;
            var returnCart = _context.Carts.Where(cart => cart.CartId == shoppingCartId);
  
            return returnCart.Include(x => x.Product).ToList();          
                
        }

        public int GetCount()
        {
            var Cart = GetCart(this.HttpContext);
            int? count =
                (from cartItems in _context.Carts where cartItems.CartId == Cart.ShoppingCartId select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            var Cart = GetCart();
            decimal? total = (from cartItems in _context.Carts
                              where cartItems.CartId == Cart.ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        //public int CreateOrder(CustomerOrder customerOrder)
        //{
        //    decimal orderTotal = 0;

        //    var cartItems = GetCartItems();

        //    foreach (var item in cartItems)
        //    {
        //        var orderedProduct = new OrderedProduct
        //        {
        //            ProductId = item.ProductId,
        //            CustomerOrderId = customerOrder.Id,
        //            Quantity = item.Count
        //        };

        //        orderTotal += (item.Count * item.Product.Price);

        //        _context.OrderedProducts.Add(orderedProduct);
        //    }

        //    customerOrder.Amount = orderTotal;

        //    _context.SaveChanges();

        //    EmptyCart();

        //    return customerOrder.Id;
        //}

        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString("CartSessionKey") == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session.SetString("CartSessionKey", context.User.Identity.Name);
                }

                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session.SetString("CartSessionKey", tempCartId.ToString());
                }
            }

            return context.Session.GetString("CartSessionKey");
        }

        public ActionResult CartSummary()
        {
            var cart = GetCart(this.HttpContext);

            ViewData["CartCount"] = GetCount();
            return PartialView("CartSummary");
        }
    }
}
