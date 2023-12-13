using AutoMapper;
using Digikala.Services.Payment.Data.Repository;
using Digikala.Services.Payment.DTO;
using Digikala.Services.Payment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentsRepository paymentsRepository,IMapper mapper)
        {
            _paymentsRepository = paymentsRepository;
            _mapper = mapper;
        }
        [HttpPost("AddPayment")]
        public async Task<IActionResult> AddPayment([FromBody]PaymentsDTO paymentsDTO)
        {
            var mypayment = new Payments
            {
                Bank = paymentsDTO.Bank,
                Checkoutid = paymentsDTO.Checkoutid,
                Status = paymentsDTO.Status,
                Userid = paymentsDTO.Userid
            };
            var paymentdata = await _paymentsRepository.AddPayments(mypayment);
            var paymentdatadto = _mapper.Map<PaymentsDTO>(paymentdata);
            return Ok(paymentdatadto);
        }
        [HttpGet("GetPayment/{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var mypayment = await _paymentsRepository.GetPayments(id);
            var mypaymentdto = _mapper.Map<PaymentsDTO>(mypayment);
            return Ok(mypaymentdto);
        }
        [HttpGet("GetPaymentbyuserid/{userid}")]
        public async Task<IActionResult> GetPaymentbyuserid(int userid)
        {
            var mypayment = await _paymentsRepository.GetPaymentsbyuserid(userid);
            var mypaymentdto = _mapper.Map<IEnumerable<PaymentsDTO>>(mypayment);
            return Ok(mypaymentdto);
        }
        [HttpGet("GetAllPayment")]
        public async Task<IActionResult> GetAllPayment()
        {
            var mypayment = await _paymentsRepository.GetallPayments();
            var mypaymentdto = _mapper.Map<IEnumerable<PaymentsDTO>>(mypayment);
            return Ok(mypaymentdto);
        }
    }
}
