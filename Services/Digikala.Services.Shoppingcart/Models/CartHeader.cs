using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Models
{
    public class CartHeader
    {
        [Key]
        public int id { get; set; }
        public int Userid { get; set; }
        public int digicouponId { get; set; }
    }
}
