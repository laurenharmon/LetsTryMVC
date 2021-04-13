using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LetsTryMVC.Models
{
    public class Friend
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }

        public Friend(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }

        public Friend() { }
    }
}
