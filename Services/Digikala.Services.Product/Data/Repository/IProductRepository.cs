using Digikala.Services.Product.Helper;
using Digikala.Services.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
    public interface IProductRepository
    {
       public void addProduct(Products product);
       public Task<PagedList<Products>> GetProductsbyCategory(string name, UserParams userParams);
       public Task<Products> GetProductbyname(string name);
        public Task<Products> GetProductsbyid(int id);
    }
}
