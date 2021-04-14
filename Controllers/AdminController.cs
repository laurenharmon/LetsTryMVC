using LetsTryMVC.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Controllers
{
    public class AdminController : Controller
    {
        //Figure This Out Sometime
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
           

            return View();
        }

    }
}
