using AutoMapper;
using Digikala.Services.Shoppingcart.Data.Repository;
using Digikala.Services.Shoppingcart.DTO;
using Digikala.Services.Shoppingcart.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartdetailRepository _cartdetailRepository;
        private readonly ICartheaderRepository _cartheader;
        private readonly IMapper _mapper;

        public CartController(IProductRepository productRepository,ICartdetailRepository cartdetailRepository,ICartheaderRepository cartheader,IMapper mapper)
        {
            _productRepository = productRepository;
            _cartdetailRepository = cartdetailRepository;
            _cartheader = cartheader;
            _mapper = mapper;
        }
        [HttpPost("AddShoppingcart")]
        public async Task<IActionResult> AddShoppingcart([FromBody]CartDetailDTO cartDetailDTO)
        {
            var myproduct = new Product
            {
                Name = cartDetailDTO.productid.Name,
                Picture = cartDetailDTO.productid.Picture,
                Price = cartDetailDTO.productid.Price
            };
            var dataproduct = await _productRepository.Addproduct(myproduct);
            var mycartheader = new CartHeader
            {
                digicouponId = cartDetailDTO.Headerid.digicouponId,
                Userid = cartDetailDTO.Headerid.Userid
            };
            var datacartheader=await _cartheader.AddCartheader(mycartheader);
            var mycartdetail = new Cartdetail
            {
                Count = cartDetailDTO.Count,
                Headerid = datacartheader,
                productid = dataproduct
            };
            var datacartdetail = await _cartdetailRepository.AddCartdetail(mycartdetail);
            return Ok(datacartdetail);
        }
        [HttpGet("GetAllShoppingcart/{userid}")]
        public async Task<IActionResult> GetAllShoppingcart(int userid)
        {
            var myshop = await _cartdetailRepository.GetAllCartdetail(userid);
            var myshopdto = _mapper.Map<IEnumerable<CartDetailDTO>>(myshop);
            return Ok(myshopdto);
        }
        [HttpGet("GetShoppingcart/{detailid}")]
        public async Task<IActionResult> GetShoppingcart(int detailid)
        {
            var myshop = await _cartdetailRepository.GetCartdetail(detailid);
            var myshopdto = _mapper.Map<CartDetailDTO>(myshop);
            return Ok(myshopdto);
        }

    }
}
