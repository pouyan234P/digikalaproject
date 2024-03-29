﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Identity.Models
{
    public class User:IdentityUser<int>
    {
        public virtual ICollection<UserRole> userRoles { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string family { get; set; }
        public string Country { get; set; }
        public string Url { get; set; }
    }
}
