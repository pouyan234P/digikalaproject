using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Models
{
    public class UserPoint
    {
        public int id { get; set; }
        public Pointofview Pointofiviewid { get; set; }
        public int Userid { get; set; }
        public Products Productid { get; set; }
    }
}
