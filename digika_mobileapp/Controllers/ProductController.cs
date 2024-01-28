
using digika_mobileapp.Helper;
using digika_mobileapp.Models;
using digika_mobileapp.Models.Productmodel;
using digika_mobileapp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static IList<IFormFile> mypicture;
        public static IFormFile mainpicture;
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetProductId/{id}")]
        public async Task<IActionResult> GetProductId(int id)
        {
           
           ProductDTO list=new();
            var response = await _productService.GetProductbyIdAsync<ResponseDTO>(id);
            if(response!=null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            }
            return Ok(list);
        }
        [HttpGet("Getbyserch/{name}")]
        public async Task<IActionResult> Getbyserch(string name,[FromQuery]UserParams userParams)
        {
            List<ProductDTO> list = new();
            var response = await _productService.SearchAsync<ResponseDTO>(name,userParams);

            if(response!=null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }
            Response.AddPagination((int) response.currentPage, (int)response.itemsPerPage, (int)response.totalItems, (int)response.totalPages);
            return Ok(list);
        }
        [HttpPost("Picture")]
        public async Task<IActionResult> Picture([FromForm]IList<IFormFile> picture,IFormFile main)
        {
            var response = await _productService.SendpictureAsync<ResponseDTO>(main);
            var response2 = await _productService.SendseconpictureAsync<ResponseDTO>(picture);
            if(response.IsSuccess && response2.IsSuccess)
            {
                return Ok(true);
            }
            return Ok(response.ErrorMessages);
        }
        [HttpPost("Addproduct")]
        public async Task<IActionResult> Addproduct([FromBody]SetProductDTO setProductDTO)
        {
            /*setProductDTO.MainPictureUrl = mainpicture;
            setProductDTO.PictureUrl = mypicture;*/
            var response = await _productService.CreateProductAsync<ResponseDTO>(setProductDTO);
            if(response.IsSuccess)
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
