using digika_mobileapp.Models;
using digika_mobileapp.Models.Identity;
using digika_mobileapp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO registerDTO)
        {
            var response = await _identityService.Register<ResponseDTO>(registerDTO);
            if (response.IsSuccess)
            {
                return Ok(true);
            }
            return BadRequest(false);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDTO userForLoginDTO)
        {
            var response = await _identityService.Login<ResponseDTO>(userForLoginDTO);
            if(response.IsSuccess)
            {
                return Ok( response.Result);
            }
            return BadRequest();
        }
    }
}
