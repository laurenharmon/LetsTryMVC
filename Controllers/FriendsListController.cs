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
    public class FriendsListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FriendsListController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var friends = _context.Users
                .ToList();
            var self = friends.Find(x => x.UserName == User.Identity.Name);
            friends.Remove(self);
            return View(friends);
        }

        public Microsoft.AspNetCore.Identity.IdentityUser GetUser(HttpContext context)
        {
            var user = _context.Users.First(p => p.UserName == User.Identity.Name);    
            return user;
        }

        public Microsoft.AspNetCore.Identity.IdentityUser GetUser()
        {
            return GetUser(this.HttpContext);
        }

        [HttpGet]
        public IActionResult AddFriend(string id)
        {

            if (id == null) {
                return Redirect("Index");
            }

            var friend = _context.Users
                .First(m => m.Id == id);

            Friend bestie = new Friend
            {
                UserId = friend.Id,
                UserName = friend.UserName
            };

            List<Friend> newList = new List<Friend>();
            newList.Add(bestie);

            AddFriendViewModel addFriendViewModel = new AddFriendViewModel(newList);

            return View(addFriendViewModel);
        }

        [HttpPost, ActionName("AddFriend")]
        public IActionResult AddFriendConfirm(AddFriendViewModel viewModel)
        {
            var user = GetUser();
            var newFriend = viewModel.Friend;
            var list = _context.FriendsLists.FirstOrDefault(x => x.Reference == user.Id);
            if (list == null)
            {
                list = new FriendsList
                {
                    Reference = user.Id,
                    Friends = viewModel.Friends 
                };
                _context.FriendsLists.Add(list);
            } else
            {
                list.Friends.Add(newFriend);
            }
           

            return RedirectToAction("MyFriends");
        }

        public IActionResult MyFriends()
        {
            var user = GetUser();
            var friends = _context.FriendsLists.First(x => x.Reference == user.Id);
                
            return View(friends);
        }
    }
}
