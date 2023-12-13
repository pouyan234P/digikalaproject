using Digikala.Services.Shoppingcart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingcartDatacontext _db;

        public ProductRepository(ShoppingcartDatacontext db)
        {
            _db = db;
        }
        public async Task<Product> Addproduct(Product product)
        {
            await _db.products.AddAsync(product);
            _db.SaveChanges();
            var myproduct = await _db.products.Select(x => x).LastOrDefaultAsync();
            return product;
        }

        public async Task<Product> GetProduct(int id)
        {
            var myproduct = await _db.products.Where(x => x.id == id).Select(x => x).FirstOrDefaultAsync();
            return myproduct;
        }
    }
}
