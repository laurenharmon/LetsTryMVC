using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    //[Bind(Exclude = "Id")]
    public class CustomerOrder
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string FirstName { get; set; }
 
        public string LastName { get; set; }
  
        public string Address { get; set; }
   
        public string City { get; set; }
    
        public string State { get; set; }
     
        public string PostalCode { get; set; }
   
        public string Country { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }

        [ScaffoldColumn(false)]
        public Decimal Amount { get; set; }

        public CustomerOrder(string firstName, string lastName, string address, string city, string state, string postalCode, string country, string phone, string email, DateTime dateCreated, decimal amount)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;
            Email = email;
            DateCreated = dateCreated;
            Amount = amount;
        }
    }
}
