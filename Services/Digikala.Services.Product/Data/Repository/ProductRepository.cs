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

        public async Task<IEnumerable<Products>> GetProductsbyCategory(string name)
        {

            /*  var categoryAndSubcategories =await _db.categories
           .Include(c => c.Subcategories)
           .Where(c => c.CategoryName == name)
           .ToListAsync();

              var productQuery = categoryAndSubcategories
                  .SelectMany(c => c.Products);

              var products = productQuery.ToList();
              return products;*/
            var query = from c in _db.categories
                        join p in _db.products on c.ID equals p.Categoryid.CategoryParent
                        join c2 in _db.categories on c.CategoryParent equals c2.ID
                        where (c2.CategoryName == name 
                        ||
                         c.CategoryName==name)
                         ||
                         p.Name.Contains(name)
                        select p;

            var products = await query.Include(t=>t.Categoryid).ToListAsync();
            return products;
           
           
        }

        public async Task<Products> GetProductsbyid(int id)
        {
            var myproduct =await _db.products.Where(x=>x.id==id).Select(x=>x).Include(t=>t.Categoryid).FirstOrDefaultAsync();
            return myproduct;
        }
    }
}
