using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LetsTryMVC.Models;

namespace LetsTryMVC.ViewModels
{
    public class CustomerOrderViewModel
    {

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
   
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
  
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone is required")]
 
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
      
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }

        public Decimal Amount { get; set; }

        public CustomerOrderViewModel(decimal amount)
        {
            Amount = Convert.ToDecimal(amount.ToString("F",
                  CultureInfo.InvariantCulture));
        }

        public CustomerOrderViewModel() { }
    }
}
