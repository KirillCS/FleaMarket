using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FleaMarket.Models
{
    public class User : IdentityUser
    {
        public List<Item> Items { get; set; }

        public User(string userName) : base(userName) { }
    }
}
