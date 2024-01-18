using digika_mobileapp.Models;
using digika_mobileapp.Models.Productmodel;
using digika_mobileapp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserpointController : ControllerBase
    {
        private readonly IUserpointService _userpointService;

        public UserpointController(IUserpointService userpointService)
        {
            _userpointService = userpointService;
        }
        [HttpPost("addUserpoint")]
        public async Task<IActionResult> addUserpoint([FromBody]GetUserPointDTO getUserPointDTO)
        {
            var response = await _userpointService.addUserpoint<ResponseDTO>(getUserPointDTO);
            if(response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.ErrorMessages);
        }
        [HttpGet("GetUserpoints/{productid}")]
        public async Task<IActionResult> GetUserpoints(int productid)
        {
            List<GetUserPointDTO> list = new();
            var response = await _userpointService.GetUserpoints<ResponseDTO>(productid);
            if(response!=null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<GetUserPointDTO>>(Convert.ToString(response.Result));
                return Ok(list);
            }
            return BadRequest(response.ErrorMessages);
        }
        [HttpGet("GetUserpoint/{userpointid}")]
        public async Task<IActionResult> GetUserpoint(int userpointid)
        {
            GetUserPointDTO list = new();
            var response = await _userpointService.GetUserpoint<ResponseDTO>(userpointid);
            if(response!=null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<GetUserPointDTO>(Convert.ToString(response.Result));
                return Ok(list);
            }
            return BadRequest(response.ErrorMessages);
        }
    }
}
