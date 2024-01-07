using digika_mobileapp.Models;
using digika_mobileapp.Models.Shoppingmodel;
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
    public class ShoppingController : ControllerBase
    {
        private readonly IShoppingService _shoppingService;

        public ShoppingController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }
        [HttpPost("Addcart")]
        public async Task<IActionResult> Addcart([FromBody]CartDetailDTO cartDetailDTO)
        {
            CartDetailDTO list = new();
          var response=  await _shoppingService.AddShoppingcart<ResponseDTO>(cartDetailDTO);
            if(response.IsSuccess && response!=null )
            {
                list = JsonConvert.DeserializeObject<CartDetailDTO>(Convert.ToString(response.Result));
                return Ok(list);
            }
            return Ok(response.DisplayMessage);
        }
        [HttpGet("GetAllShoppingcart/{userid}")]
        public async Task<IActionResult> GetAllShoppingcart(int userid)
        {
            List<CartDetailDTO> list = new();
            var response = await _shoppingService.GetAllShoppingcart<ResponseDTO>(userid);
            if(response!=null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CartDetailDTO>>(Convert.ToString(response.Result));
            }
            return Ok(list);
        }
        [HttpGet("GetShoppingcart/{detailid}")]
        public async Task<IActionResult> GetShoppingcart(int detailid)
        {
            CartDetailDTO list=new();
            var response = await _shoppingService.GetShoppingcart<ResponseDTO>(detailid);
            if(response!=null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<CartDetailDTO>(Convert.ToString(response.Result));
            }
            return Ok(list);
        }
    }
}
