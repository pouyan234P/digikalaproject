using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.CouponApi.Models
{
    public class digicoupon
    {
        [Key]
        public int DigicouponID { get; set; }
        public int Amount { get; set; }
    }
}
