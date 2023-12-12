using AutoMapper;
using Digikala.Services.Checkout.Data.Repository;
using Digikala.Services.Checkout.DTO;
using Digikala.Services.Checkout.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Checkout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ISummaryRepository _summaryRepository;
        private readonly IMapper _mapper;

        public CheckoutController(ISummaryRepository summaryRepository,IMapper mapper)
        {
            _summaryRepository = summaryRepository;
            _mapper = mapper;
        }
        [HttpPost("AddSummery")]
        public async Task<IActionResult> AddSummery([FromBody]SummaryDTO summaryDTO)
        {
            var mysummary = new Summary
            {
                date = DateTime.Now,
                Giftcardid = summaryDTO.Giftcardid,
                Shoppingcartid = summaryDTO.Shoppingcartid,
                Userid = summaryDTO.Userid
            };
            var datasummary = await _summaryRepository.Addsummary(mysummary);
            return Ok(datasummary);
        }
        public async Task<IActionResult> GetSummery(int userid)
        {
            return Ok();
        }
    }
}
