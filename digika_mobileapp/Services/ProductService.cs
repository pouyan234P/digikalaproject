using digika_mobileapp.Models;
using digika_mobileapp.Models.Productmodel;
using digika_mobileapp.Services.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace digika_mobileapp.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory):base (clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> CreateProductAsync<T>(SetProductDTO setProductDTO)
        {
          return  await this.SendAsync<T>(new ApiRequest
            {
                ApiType=SD.ApiType.POST,
                Data=setProductDTO,
                Url=SD.ProductApiBase+ "/api/Products/addProduct",
                AccessToken=""
            });
        }

        public async Task<T> GetProductbyIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductApiBase + "/api/Products/Getproduct/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> SearchAsync<T>(string name)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url =SD.ProductApiBase+ "/api/Products/SearchbyCategory/"+name,
                AccessToken=""
            });
        }

        public async Task<T> SendpictureAsync<T>(IFormFile mainpicture)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductApiBase + "/api/Products/SetPicture",
                file = mainpicture,
                AccessToken = ""
            });
        }

        public async Task<T> SendseconpictureAsync<T>(IList<IFormFile> mypictures)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductApiBase + "/api/Products/Setseconpicture",
                Ifile = mypictures,
                AccessToken = ""
            });
        }
    }
}
