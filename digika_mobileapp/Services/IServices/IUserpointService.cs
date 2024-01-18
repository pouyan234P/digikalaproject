using digika_mobileapp.Models.Productmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Services.IServices
{
   public interface IUserpointService:IBaseService
    {
        Task<T> addUserpoint<T>(GetUserPointDTO getUserPointDTO);
        Task<T> GetUserpoints<T>(int productid);
        Task<T> GetUserpoint<T>(int userpointid);
    }
}
