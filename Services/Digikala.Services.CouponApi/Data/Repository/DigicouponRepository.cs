using Digikala.Services.CouponApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.CouponApi.Data.Repository
{
    public class DigicouponRepository : IDigicouponRepository
    {
        private readonly Digikalacoupon _digikalacoupon;

        public DigicouponRepository(Digikalacoupon digikalacoupon)
        {
            _digikalacoupon = digikalacoupon;
        }
        public async void Adddigicoupon(digicoupon digicoupon)
        {
            await _digikalacoupon.digicoupons.AddAsync(digicoupon);
            _digikalacoupon.SaveChanges();
        }
    }
}
