using LetsTryMVC.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Controllers
{
    public class FriendsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FriendsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var friends = _context.Users.ToList();
            return View(friends);
        }

        public void AddFriend(int id)
        {
            
        }
    }
}
