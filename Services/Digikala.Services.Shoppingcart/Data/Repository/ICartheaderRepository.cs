using Digikala.Services.Shoppingcart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Data.Repository
{
   public interface ICartheaderRepository
    {
        Task<CartHeader> AddCartheader(CartHeader cartHeader);
        Task<CartHeader> GetCartHeader(int userid);
        Task<IEnumerable<CartHeader>> GetAllCartheader();
    }
}
