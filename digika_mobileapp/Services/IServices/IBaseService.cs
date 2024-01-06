using digika_mobileapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Services.IServices
{
   public interface IBaseService:IDisposable
    {
        ResponseDTO responseDTO { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
