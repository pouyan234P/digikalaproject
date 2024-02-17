using digika_mobileapp.Helper;
using digika_mobileapp.Models;
using digika_mobileapp.Models.Productmodel;
using digika_mobileapp.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserpointController : ControllerBase
    {
        private readonly IUserpointService _userpointService;

        public UserpointController(IUserpointService userpointService)
        {
            _userpointService = userpointService;
        }
        [Authorize]
        [HttpPost("addUserpoint")]
        public async Task<IActionResult> addUserpoint([FromBody]GetUserPointDTO getUserPointDTO)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _userpointService.addUserpoint<ResponseDTO>(getUserPointDTO,accessToken);
            if(response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.ErrorMessages);
        }
        [AllowAnonymous]
        [HttpGet("GetUserpoints/{productid}")]
        public async Task<IActionResult> GetUserpoints(int productid,[FromQuery]UserParams userParams)
        {
            List<GetUserPointDTO> list = new();
            var response = await _userpointService.GetUserpoints<ResponseDTO>(productid,userParams);
            if (response !=null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<GetUserPointDTO>>(Convert.ToString(response.Result));
                Response.AddPagination((int)response.currentPage, (int)response.itemsPerPage, (int)response.totalItems, (int)response.totalPages);
                return Ok(list);
            }
            return BadRequest();
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
