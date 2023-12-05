using AutoMapper;
using Digikala.Services.CouponApi.DTO;
using Digikala.Services.CouponApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.CouponApi.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<giftcard, GiftcardDTO>();
        }
    }
}
