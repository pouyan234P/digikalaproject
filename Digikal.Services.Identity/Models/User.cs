using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Identity.Models
{
    public class User:IdentityUser<int>
    {
        public virtual ICollection<UserRole> userRoles { get; set; }

    }
}
