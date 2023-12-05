using Digikala.Services.CouponApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.CouponApi.Data.Repository
{
   public interface IGiftcardRepository
    {
        public void AddGiftcard(giftcard giftcard);
        public Task<giftcard> GetGiftcard(int giftcode);
    }
}
