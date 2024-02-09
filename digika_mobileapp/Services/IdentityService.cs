using digika_mobileapp.Models;
using digika_mobileapp.Models.Identity;
using digika_mobileapp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace digika_mobileapp.Services
{
    public class IdentityService : BaseService, IIdentityService
    {
        private readonly IHttpClientFactory _clientFactory;

        public IdentityService(IHttpClientFactory clientFactory):base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> Login<T>(UserForLoginDTO userForLoginDTO)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType=SD.ApiType.POST,
                Data=userForLoginDTO,
                Url=SD.Identity+ "/api/Account/Login"
            });
        }

        public async Task<T> Register<T>(RegisterDTO registerDTO)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType=SD.ApiType.POST,
                Data=registerDTO,
                Url = SD.Identity + "/api/Account/Register"
            });
        }
    }
}
