using LetsTryMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.ViewModels
{
    public class AddFriendViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public Friend Friend { get; set; }
        public List<Friend> Friends { get; set; }
        public AddFriendViewModel(List<Friend> friends)
        {
            foreach (Friend friend in friends)
            {
                Friends.Add(friend);
            }

        }

        public AddFriendViewModel() { }

    }
}
