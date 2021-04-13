using LetsTryMVC.Data;
using LetsTryMVC.Models;
using LetsTryMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public UserProfile GetUser()
        {
            var user = _context.Users.First(p => p.UserName == User.Identity.Name);
            var doesUserHaveProfile = _context.Profiles.FirstOrDefault(u => u.UserName == user.UserName);
            
            if (doesUserHaveProfile == null)
            {
                UserProfile profile = new UserProfile
                {
                    UserName = user.UserName
                };
                _context.Profiles.Add(profile);
                _context.SaveChanges();               
                return profile;
            } else
            {
                return doesUserHaveProfile;
            }

        }


        public UserProfile GetFriend(string id)
        {
            var user = _context.Users.First(p => p.Id == id);
            var doesFriendHaveProfile = _context.Profiles.FirstOrDefault(u => u.UserName == user.UserName);

            if (doesFriendHaveProfile == default)
            {
                UserProfile friendProfile = new UserProfile
                {
                    UserName = user.UserName
                };
                _context.Profiles.Add(friendProfile);
                _context.SaveChanges();
                return friendProfile;
            }
            return doesFriendHaveProfile;
        }

        public int GetRelationId(UserProfile user, UserProfile friend)
        {
            var areFriends = _context.Friends.FirstOrDefault(x => x.UserId == user.Id && x.FriendId == friend.Id);
            return areFriends.RelationId;
        }

        public IActionResult AddFriend(string id)
        {
            var user = GetUser();
            var newFriend = GetFriend(id);

            Friends besties = new Friends
            {
                UserId = user.Id,
                FriendId = newFriend.Id
            };
            _context.Friends.Add(besties);
            _context.SaveChanges();
            return RedirectToAction("MyFriends", "FriendsList", id);
        }

        public IActionResult MyFriends()
        {
            var user = GetUser();
            var myFriendships = _context.Friends.Where(x => x.UserId == user.Id || x.FriendId == user.Id)
                .Include(x => x.User)
                .Include(x => x.Friend)
                .ToList();
            var self = myFriendships.Find(x => x.Friend.UserName == user.UserName);
            myFriendships.Remove(self);

            return View(myFriendships);
        }

        public IActionResult RemoveFriend(int id)
        {

        }
    }
}
