using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


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

        [Required(ErrorMessage = "Card Number is required")]
        [StringLength(19, MinimumLength = 13, ErrorMessage = "Please enter a valid credit card number with no spaces or dashes.")]

        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiration Date is required")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Expiration Date must be MM/YYYY")]
        public string Expiration { get; set; }

        [Required(ErrorMessage = "Security Code is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Security Code must be 3 digits.")]
        public string SecCode { get; set; }

        [Required(ErrorMessage = "Card Owner's name is required.")]
        public string CardOwner { get; set; }
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
