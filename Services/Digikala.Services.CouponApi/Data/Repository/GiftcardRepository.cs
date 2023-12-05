using Digikala.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.CouponApi.Data.Repository
{
    public class GiftcardRepository : IGiftcardRepository
    {
        private readonly Digikalacoupon _digikalacoupon;

        public GiftcardRepository(Digikalacoupon digikalacoupon)
        {
            _digikalacoupon = digikalacoupon;
        }
        public async void AddGiftcard(giftcard giftcard)
        {
            await _digikalacoupon.giftcards.AddAsync(giftcard);
            _digikalacoupon.SaveChanges();
        }

        public async Task<giftcard> GetGiftcard(int giftcode)
        {
            var mygiftcard = await _digikalacoupon.giftcards.Where(t => t.GiftcartCode == giftcode).Select(t => t).FirstOrDefaultAsync();
            return mygiftcard;
        }
    }
}
