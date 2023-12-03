using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Identity.DTO
{
    public class GetUserDTO
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string family { get; set; }
        public string Role { get; set; }

    }
}
