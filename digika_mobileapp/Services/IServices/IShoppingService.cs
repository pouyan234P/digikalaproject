using digika_mobileapp.Models.Shoppingmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Services.IServices
{
   public interface IShoppingService:IBaseService
    {
        Task<T> AddShoppingcart<T>(CartDetailDTO CartDetailDTO);
        Task<T> GetAllShoppingcart<T>(int userid);
        Task<T> GetShoppingcart<T>(int detailid);
    }
}
