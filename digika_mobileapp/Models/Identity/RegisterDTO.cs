using Digikala.Services.Identity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Models.Identity
{
    public class RegisterDTO
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string family { get; set; }
        public string Country { get; set; }
        public string phoneNumber { get; set; }
  
    }
}
