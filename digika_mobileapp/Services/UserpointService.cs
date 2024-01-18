using digika_mobileapp.Models;
using digika_mobileapp.Models.Productmodel;
using digika_mobileapp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace digika_mobileapp.Services
{
    public class UserpointService : BaseService, IUserpointService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserpointService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<T> addUserpoint<T>(GetUserPointDTO getUserPointDTO)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType=SD.ApiType.POST,
                Data=getUserPointDTO,
                Url=SD.ProductApiBase+ "/api/Userpoint/addUserpoint"
            });
        }

        public async Task<T> GetUserpoint<T>(int userpointid)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType=SD.ApiType.GET,
                Url=SD.ProductApiBase+ "/api/Userpoint/GetUserpoint/"+userpointid
            });
        }

        public async Task<T> GetUserpoints<T>(int productid)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType=SD.ApiType.GET,
                Url=SD.ProductApiBase+ "/api/Userpoint/GetUserpoints/"+productid
            });
        }
    }
}
