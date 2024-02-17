using AutoMapper;
using Digikala.Services.Shoppingcart.Data.Repository;
using Digikala.Services.Shoppingcart.DTO;
using Digikala.Services.Shoppingcart.Helper;
using Digikala.Services.Shoppingcart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        protected ResponseDTO _response;
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
            _response = new();
        }
        [HttpPost("AddShoppingcart")]
        public async Task<IActionResult> AddShoppingcart([FromBody] CartDetailDTO cartDetailDTO)
        {
            try
            {
                var myproduct = new Product
                {
                    Name = cartDetailDTO.productid.Name,
                    Picture = cartDetailDTO.productid.Picture,
                    Price = cartDetailDTO.productid.Price,
                    productid=cartDetailDTO.productid.productid,
                    Color=cartDetailDTO.productid.Color,
                    hashColor=cartDetailDTO.productid.hashColor
                };
                var dataproduct = await _productRepository.Addproduct(myproduct);
                var mycartheader = new CartHeader
                {
                    digicouponId = cartDetailDTO.Headerid.digicouponId,
                    Userid = cartDetailDTO.Headerid.Userid
                };
                var datacartheader = await _cartheader.AddCartheader(mycartheader);
                var mycartdetail = new Cartdetail
                {
                    Count = cartDetailDTO.Count,
                    Headerid = datacartheader,
                    productid = dataproduct
                };
                var datacartdetail = await _cartdetailRepository.AddCartdetail(mycartdetail);
                _response.Result = datacartdetail;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return Ok(_response);
        }
        [HttpGet("GetAllShoppingcart/{userid}")]
        public async Task<IActionResult> GetAllShoppingcart(int userid,[FromQuery]UserParams userParams)
        {
            try
            {
                var myshop = await _cartdetailRepository.GetAllCartdetail(userid,userParams);
                var myshopdto = _mapper.Map<IEnumerable<CartDetailDTO>>(myshop);
                _response.Result = myshopdto;
                _response.currentPage = myshop.CurrentPage;
                _response.itemsPerPage = myshop.PageSize;
                _response.totalItems = myshop.TotalCount;
                _response.totalPages = myshop.TotalPage;
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return Ok(_response);
        }
        [HttpGet("GetShoppingcart/{detailid}")]
        public async Task<IActionResult> GetShoppingcart(int detailid)
        {
            try
            {
                var myshop = await _cartdetailRepository.GetCartdetail(detailid);
                var myshopdto = _mapper.Map<CartDetailDTO>(myshop);
                _response.Result = myshopdto;
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return Ok(_response);
        }
        [HttpPost("deleteShoppingcart/{detailid}")]
        public async Task<IActionResult> deleteShoppingcart(int detailid)
        {
            try
            {
                var shop = await _cartdetailRepository.deleteCartdetail(detailid);
                _response.Result = true;

            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return Ok(_response);

        }

    }
}
