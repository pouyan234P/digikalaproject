using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.DTO
{
    public class CartHeaderDTO
    {
        public int id { get; set; }
        public int Userid { get; set; }
        public int digicouponId { get; set; }
    }
}
