using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public class FriendsList
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public List<Friend> Friends { get; set; }

        public FriendsList() { }

    }
}
