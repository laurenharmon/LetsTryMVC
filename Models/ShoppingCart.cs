using LetsTryMVC.Controllers;
using LetsTryMVC.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public class ShoppingCart
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "cartId";

        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();

            cart.ShoppingCartId = cart.GetCartId(context);

            return cart;
        }

        public static ShoppingCart GetCart(ShoppingCartController controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product product)
        {
            var cartItem = _context.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.ProductId == product.Id);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductId = product.Id,
                    CartId = ShoppingCartId,
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

        public int RemoveFromCart(int id)
        {
            var cartItem = _context.Carts.SingleOrDefault(cart => cart.CartId == ShoppingCartId && cart.ProductId == id);

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

        public void EmptyCart()
        {
            var cartItems = _context.Carts.Where(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _context.Carts.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return _context.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            int? count =
                (from cartItems in _context.Carts where cartItems.CartId == ShoppingCartId select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in _context.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(CustomerOrder customerOrder)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderedProduct = new OrderedProduct
                {
                    ProductId = item.ProductId,
                    CustomerOrderId = customerOrder.Id,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Product.Price);

                _context.OrderedProducts.Add(orderedProduct);
            }

            customerOrder.Amount = orderTotal;

            _context.SaveChanges();

            EmptyCart();

            return customerOrder.Id;
        }

        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session.SetString(CartSessionKey, context.User.Identity.Name);
                }

                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }

            return context.Session.GetString(CartSessionKey).ToString();
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = _context.Carts.Where(c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }

            _context.SaveChanges();
        }

    }
}
