using AutoMapper;
using Digikala.Services.Product.Data.Repository;
using Digikala.Services.Product.DTO;
using Digikala.Services.Product.Helper;
using Digikala.Services.Product.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserpointController : ControllerBase
    {
        protected ResponseDTO _response;
        private readonly IUserpointRepository _userpointRepository;
        private readonly IMapper _mapper;

        public UserpointController(IUserpointRepository userpointRepository,IMapper mapper)
        {
            _userpointRepository = userpointRepository;
            _mapper = mapper;
            this._response = new ResponseDTO();
        }
        [Authorize]
        [HttpPost("addUserpoint")]
        public async Task<IActionResult> addUserpoint([FromBody]GetUserPointDTO getUserPointDTO)
        {
            try
            {
                var mypointofiview = new Pointofview
                {
                    Commenttext = getUserPointDTO.Pointofviewid.Commenttext,
                    Commenttitle = getUserPointDTO.Pointofviewid.Commenttitle,
                    Score = getUserPointDTO.Pointofviewid.Score
                };
                string joinedpositive = string.Join("\n", getUserPointDTO.Pointofviewid.Positivepoints);
                mypointofiview.Positivepoints = Encoding.UTF8.GetBytes(joinedpositive);
                string joinnegative = string.Join("\n", getUserPointDTO.Pointofviewid.Negativepoints);
                mypointofiview.Negativepoints = Encoding.UTF8.GetBytes(joinnegative);
                var myproduct = new Products
                {
                    id = getUserPointDTO.Productid.id
                };
                var myuserpoint = new UserPoint
                {
                    Pointofiviewid=mypointofiview,
                    Userid=getUserPointDTO.Userid,
                    Productid = myproduct
                };
               await _userpointRepository.AddUserpoint(myuserpoint);
               
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return Ok(_response);
        }
        [AllowAnonymous]
        [HttpGet("GetUserpoints/{productid}")]
        public async Task<IActionResult> GetUserpoints(int productid,[FromQuery]UserParams userParams)
        {
           try
            {
                var myuserpoint = await _userpointRepository.GetUserPoints(productid,userParams);
               
                //IEnumerable<UserPoint> enumerable = (IEnumerable<UserPoint>)myuserpoint.ToList();

                var myuserpointdto = _mapper.Map<IEnumerable<GetUserPointDTO>>(myuserpoint);
               
                if(myuserpointdto.Count()==0)
                {
                    _response.IsSuccess=false;
                    return BadRequest();
                }
                _response.Result = myuserpointdto;
                _response.currentPage = myuserpoint.CurrentPage;
                _response.itemsPerPage = myuserpoint.PageSize;
                _response.totalItems = myuserpoint.TotalCount;
                _response.totalPages = myuserpoint.TotalPage;
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return Ok(_response);
        }
        [HttpGet("GetUserpoint/{id}")]
        public async Task<IActionResult> GetUserpoint(int id)
        {
            try
            {
                var myuserpoint = await _userpointRepository.GetUserPoint(id);
                var myuserpointdto = _mapper.Map<GetUserPointDTO>(myuserpoint);
                _response.Result = myuserpointdto;
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
