using digika_mobileapp.Helper;
using digika_mobileapp.Models;
using digika_mobileapp.Models.Shoppingmodel;
using digika_mobileapp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace digika_mobileapp.Services
{
    public class ShoppingService : BaseService, IShoppingService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ShoppingService(IHttpClientFactory clientFactory):base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> AddShoppingcart<T>(CartDetailDTO CartDetailDTO, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType=SD.ApiType.POST,
                Url=SD.ShoppingApiBase + "/api/Cart/AddShoppingcart",
                Data=CartDetailDTO,
                AccessToken=token
            });
        }

        public async Task<T> DeleteShoppingcart<T>(int detailid, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType=SD.ApiType.POST,
                Url=SD.ShoppingApiBase + "/api/Cart/deleteShoppingcart/"+ detailid,
                AccessToken=token
            });
        }

        public async Task<T> GetAllShoppingcart<T>(int userid,UserParams userParams, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingApiBase + "/api/Cart/GetAllShoppingcart/" + userid + "?" + "PageNumber=" + userParams.PageNumber,
                AccessToken = token
            });
        }

        public async Task<T> GetShoppingcart<T>(int detailid, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingApiBase + "/api/Cart/GetShoppingcart/" + detailid,
                AccessToken = token
            });
        }
    }
}
