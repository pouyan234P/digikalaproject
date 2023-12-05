using AutoMapper;
using Digikala.Services.CouponApi.Data.Repository;
using Digikala.Services.CouponApi.DTO;
using Digikala.Services.CouponApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IDigicouponRepository _digicouponRepository;
        private readonly IGiftcardRepository _giftcardRepository;
        private readonly IMapper _mapper;

        public CouponController(IDigicouponRepository digicouponRepository,IGiftcardRepository giftcardRepository,IMapper mapper)
        {
            _digicouponRepository = digicouponRepository;
            _giftcardRepository = giftcardRepository;
            _mapper = mapper;
        }
        [HttpPost("Adddigicoupon")]
        public async Task<IActionResult> Adddigicoupon([FromBody]DigiCouponDTO digiCouponDTO)
        {
            var mydigicoupon = new digicoupon
            {
                Amount = digiCouponDTO.Amount
            };
            _digicouponRepository.Adddigicoupon(mydigicoupon);
            return Ok();
        }
        [HttpPost("AddGiftcart")]
        public async Task<IActionResult> AddGiftcart([FromBody]GiftcardDTO giftcardDTO)
        {
            var mygiftcard = new giftcard
            {
                Amount = giftcardDTO.Amount,
                GiftcartCode=giftcardDTO.GiftcartCode
            };
            _giftcardRepository.AddGiftcard(mygiftcard);
            return Ok();
        }
        [HttpGet("GetGiftcard/{giftcode}")]
        public async Task<IActionResult> GetGiftcard(int giftcode)
        {
            var mygiftcart = _giftcardRepository.GetGiftcard(giftcode);
            var mygiftcartdto = _mapper.Map<GiftcardDTO>(mygiftcart);
            return Ok(mygiftcartdto);
        }

    }
}
