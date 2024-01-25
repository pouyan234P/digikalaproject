using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Digikala.Services.Product.Data.Repository;
using Digikala.Services.Product.DTO;
using Digikala.Services.Product.Helper;
using Digikala.Services.Product.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        protected ResponseDTO _response;
        public static ImageUploadResult? myuploadresult1 { get; set; }
        public static byte[]? Myurl { get; set; }
        public static byte[]? MyurlID { get; set; }
        private readonly IProductRepository _productRepository;
        private readonly IMongoInformationDBContext _informationdb;
        private readonly IOptions<CloudinarySetting> _cloudinaryconfig;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private Cloudinary _cloudinary;
        public ProductsController(IProductRepository productRepository,IMongoInformationDBContext informationdb, IOptions<CloudinarySetting> cloudinaryconfig,ICategoryRepository categoryRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _informationdb = informationdb;
            _cloudinaryconfig = cloudinaryconfig;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            Account acc = new Account(_cloudinaryconfig.Value.CloudName, _cloudinaryconfig.Value.ApiKey, _cloudinaryconfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
            this._response = new ResponseDTO();
        }
        [HttpPost("SetPicture")]
        public  async Task<IActionResult> SetPicture(IFormFile mainpicture)
        {
            try
            {
                var file1 = mainpicture;
                myuploadresult1 = new ImageUploadResult();
                if (file1.Length > 0)
                {
                    using (var stream = file1.OpenReadStream())
                    {
                        var uploadpram = new ImageUploadParams()
                        {
                            File = new FileDescription(file1.Name, stream)
                        };
                        myuploadresult1 = _cloudinary.Upload(uploadpram);
                    }
                }
               
              
               // MyurlID = Encoding.UTF8.GetBytes(publicID.ToString());
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return Ok(_response);
          
        }
        [HttpPost("Setseconpicture")]
        public IActionResult Setseconpicture(IList<IFormFile> mypicture)
        {
            try
            {
                StringBuilder publicID = new StringBuilder();
                StringBuilder sb = new StringBuilder();
                foreach (var item in mypicture)
                {
                    var file = item;
                    var uploadresult = new ImageUploadResult();
                    if (file.Length > 0)
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            var uploadpram = new ImageUploadParams()
                            {
                                File = new FileDescription(file.Name, stream)
                            };
                            uploadresult = _cloudinary.Upload(uploadpram);
                        }
                    }
                    sb.AppendLine(uploadresult.Uri.ToString());
                    publicID.AppendLine(uploadresult.PublicId.ToString());
                }
                Myurl = Encoding.UTF8.GetBytes(sb.ToString());
                MyurlID = Encoding.UTF8.GetBytes(publicID.ToString());
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return Ok(_response);
        }
        [HttpPost("addProduct")]
        public async Task<IActionResult> addProduct([FromBody]SetProductDTO setProductDTO)
        {
            try
            {
                //SetPicture(setProductDTO.PictureUrl, setProductDTO.MainPictureUrl);
                var bsonDocument = BsonDocument.Parse(setProductDTO.information.ToString());
                var data = await _informationdb.Create(bsonDocument);
                var id = data["_id"];


                var mycategory = await _categoryRepository.GetCategory(setProductDTO.Categoryid.ID);
                var product = new Products
                {
                    mainpictureUrlID = myuploadresult1.Uri.ToString(),
                    mainpicture = myuploadresult1.PublicId,
                    PictureUrlID = MyurlID,
                    pictures = Myurl,
                    Color = setProductDTO.Color,
                    Informationid = id.ToString(),
                    Insurance = setProductDTO.Insurance,
                    Name = setProductDTO.Name,
                    Nameforushghah = setProductDTO.Nameforushghah,
                    Price = setProductDTO.Price,
                    Categoryid = mycategory,
                    AverageScore=setProductDTO.averageScore,
                    HashColor=setProductDTO.hashColor
                };
                _productRepository.addProduct(product);
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return Ok(_response);
        }
        [HttpGet]
        [Route("Getproduct/{id}")]
        public async Task<IActionResult> Getproduct(int id)
        {
            try
            {
                var myproduct = await _productRepository.GetProductsbyid(id);
                var myinfo = await _informationdb.GetInformation(myproduct.Informationid);
                var myproductdto = _mapper.Map<ProductDTO>(myproduct);
                myproductdto.Informationid = myinfo.ToString();
                if (myproduct.pictures != null)
                {
                    byte[] bytes = myproduct.pictures;
                    var oneBigString = Encoding.ASCII.GetString(bytes);
                    string[] lines = oneBigString.Split('\n');
                    myproductdto.pictures = lines;
                }
                _response.Result = myproductdto;
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return Ok(_response);
        }
        [HttpGet("SearchbyCategory/{name}")]
        public async Task<IActionResult> SearchbyCategory(string name)
        {
            try
            {
                var mylist = new List<dynamic>();
                var product = await _productRepository.GetProductsbyCategory(name);
                List<Products> list = new();
                foreach (var item in product)
                {
                    list.Add(item);
                    var myinfo = await _informationdb.GetInformation(item.Informationid);
                    mylist.Add(myinfo.ToJson().Normalize());
                }
                
                var productdto = _mapper.Map<IEnumerable<ProductDTO>>(product);
                int count = 0;
                foreach (var item in productdto)
                {
                    item.Informationid = mylist[count];
                    if (list[count].PictureUrlID != null)
                    {
                        byte[] bytes = list[count].pictures;
                        var oneBigString = Encoding.ASCII.GetString(bytes);
                        string[] lines = oneBigString.Split('\n');
                        item.pictures = lines;
                    }
                    count++;
                }
                _response.Result = productdto;
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
