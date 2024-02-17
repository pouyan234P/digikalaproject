using digika_mobileapp.Helper;
using digika_mobileapp.Models.Shoppingmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Services.IServices
{
   public interface IShoppingService:IBaseService
    {
        Task<T> AddShoppingcart<T>(CartDetailDTO CartDetailDTO,string token);
        Task<T> GetAllShoppingcart<T>(int userid,UserParams userParams, string token);
        Task<T> GetShoppingcart<T>(int detailid, string token);
        Task<T> DeleteShoppingcart<T>(int detailid, string token);
    }
}
