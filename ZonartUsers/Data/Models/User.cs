using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ZonartUsers.Data.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
