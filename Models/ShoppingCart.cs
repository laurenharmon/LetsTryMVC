using LetsTryMVC.Controllers;
using LetsTryMVC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public partial class ShoppingCart
    {
        public string ShoppingCartId { get; set; }

        public static string CartSessionKey = "cartId";


    }
}

