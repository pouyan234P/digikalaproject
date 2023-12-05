using Digikala.Services.CouponApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.CouponApi.Data.Repository
{
   public interface IDigicouponRepository
    {
        public void Adddigicoupon(digicoupon digicoupon);
    }
}
