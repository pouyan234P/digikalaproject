using Digikala.Services.Product.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Digikalaproduct _db;

        public ProductRepository(Digikalaproduct db)
        {
            _db = db;
        }
        public async void addProduct(Products product)
        {
            await _db.products.AddAsync(product);
            _db.SaveChanges();
        }

        public async Task<Products> GetProductbyname(string name)
        {
            var myproduct = await _db.products.Where(x => x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase)).Select(x => x).FirstOrDefaultAsync();
            return myproduct;
        }

        public async Task<IEnumerable<Products>> GetProductsbyCategory(int categoryid)
        {
            var query = from c in _db.categories
                        join p in _db.products on c.ID equals p.Categoryid.ID
                        where c.ID == categoryid
                        select p;

            var products = await query.ToListAsync();
            return products;
        }

        public async Task<Products> GetProductsbyid(int id)
        {
            var myproduct = await _db.products.Where(x => x.id == id).Select(x => x).FirstOrDefaultAsync();
            return myproduct;
        }
    }
}
