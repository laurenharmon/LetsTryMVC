using LetsTryMVC.Data;
using LetsTryMVC.Models;
using LetsTryMVC.ViewModels;
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
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }


 
        const String PromoCode = "DEVELOPER";

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddressAndPayment(decimal amount)
        {
            CustomerOrderViewModel customerOrderViewModel = new CustomerOrderViewModel(amount);
            return View(customerOrderViewModel);
        }

        [HttpPost]
        public IActionResult AddressAndPayment(CustomerOrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {

                CustomerOrder newOrder = new CustomerOrder
                {
                    FirstName = orderViewModel.FirstName,
                    LastName = orderViewModel.LastName,
                    Address = orderViewModel.Address,
                    City = orderViewModel.City,
                    State = orderViewModel.State,
                    PostalCode = orderViewModel.PostalCode,
                    Country = orderViewModel.Country,
                    Phone = orderViewModel.Phone,
                    Email = orderViewModel.Email,
                    DateCreated = orderViewModel.DateCreated,
                    Amount = orderViewModel.Amount
                };
 
                _context.CustomerOrders.Add(newOrder);
                _context.SaveChanges();

                return RedirectToAction("EmptyCart", "ShoppingCart", new { id = newOrder.Id });
            }
            else
            {
                return RedirectToAction("AddressAndPayment");
            }
 
        }

        public IActionResult Complete(int id)
        {
            bool isValid = _context.CustomerOrders.Any(
                o => o.Id == id);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }


        }            
    }
}
