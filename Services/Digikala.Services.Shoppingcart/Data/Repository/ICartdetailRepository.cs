using Digikala.Services.Shoppingcart.Helper;
using Digikala.Services.Shoppingcart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Data.Repository
{
   public interface ICartdetailRepository
    {
        Task<Cartdetail> AddCartdetail(Cartdetail cartdetail);
        Task<Cartdetail> GetCartdetail(int id);
        Task<PagedList<Cartdetail>> GetAllCartdetail(int Userid,UserParams userParams);
    }
}
