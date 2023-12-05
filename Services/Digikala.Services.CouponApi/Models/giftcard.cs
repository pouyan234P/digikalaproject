using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.CouponApi.Models
{
    public class giftcard
    {
        [Key]
        public int GiftcardID { get; set; }
        public int GiftcartCode { get; set; }
        public int Amount { get; set; }
    }
}
