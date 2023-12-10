using Digikala.Services.Shoppingcart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Data.Repository
{
   public interface IProductRepository
    {
        Task<Product> Addproduct(Product product);
        Task<Product> GetProduct(int id);
    }
}
