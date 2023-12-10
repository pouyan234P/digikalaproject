using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Checkout.Models
{
    public class Summary
    {
        public int id { get; set; }
        public int Userid { get; set; }
        public int Shoppingcartid { get; set; }
        public DateTime date { get; set; }
        public int Giftcardid { get; set; }
    }
}
