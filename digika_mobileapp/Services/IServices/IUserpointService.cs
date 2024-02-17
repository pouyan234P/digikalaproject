using digika_mobileapp.Helper;
using digika_mobileapp.Models.Productmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Services.IServices
{
   public interface IUserpointService:IBaseService
    {
        Task<T> addUserpoint<T>(GetUserPointDTO getUserPointDTO, string token);
        Task<T> GetUserpoints<T>(int productid,UserParams userParams);
        Task<T> GetUserpoint<T>(int userpointid);
    }
}
