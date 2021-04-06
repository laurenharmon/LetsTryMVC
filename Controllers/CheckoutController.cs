using LetsTryMVC.Data;
using LetsTryMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Controllers
{
    public class CheckoutController : Controller
    {
        //private readonly ApplicationDbContext _context;

        //public CheckoutController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //const String PromoCode = "FREE";

        //public IActionResult Index()
        //{
        //    return View();
        //}

     
        //public ActionResult AddressAndPayment()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AddressAndPayment(IFormCollection values)
        //{
        //    var order = new CustomerOrder();

        //    TryUpdateModelAsync(order);

        //    try
        //    {
        //        if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
        //        {
        //            return View(order);
        //        }
        //        else
        //        {
        //            order.CustomerUserName = User.Identity.Name;
        //            order.DateCreated = DateTime.Now;

        //            _context.CustomerOrders.Add(order);
        //            _context.SaveChanges();

        //            var cart = GetCart();
        //            cart.CreateOrder(order);

        //            _context.SaveChanges();//we have received the total amount lets update it

        //            return RedirectToAction("Complete", new { id = order.Id });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.InnerException.ToString();
        //        return View(order);
        //    }
        //}

        //public ActionResult Complete(int id)
        //{
        //    bool isValid = _context.CustomerOrders.Any(
        //        o => o.Id == id);

        //    if (isValid)
        //    {
        //        return View(id);
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}
    }
}
