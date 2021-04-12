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
    public class FriendsListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FriendsListController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var friends = _context.Users.ToList();
            return View(friends);
        }

        public Microsoft.AspNetCore.Identity.IdentityUser GetUser(HttpContext context)
        {
            var list = new FriendsList();
            list.UserId = _context.Users.Where(p => p.UserName == User.Identity.Name).ToString();
            var user = _context.Users.First(p => p.UserName == User.Identity.Name);
            return user;
        }

        public Microsoft.AspNetCore.Identity.IdentityUser GetUser()
        {
            return GetUser(this.HttpContext);
        }

        public IActionResult AddFriend(string id)
        {
            var user = GetUser();

            var usersNewFriend = _context.Users.First(u => u.Id == id);

            var friendList = _context.FriendsLists.First(x => x.UserId == user.UserName);

            friendList.Friends.Add(usersNewFriend);
            _context.SaveChanges();

            return RedirectToAction("MyFriends");
        }

        public IActionResult MyFriends()
        {
            return View();
        }
    }
}
