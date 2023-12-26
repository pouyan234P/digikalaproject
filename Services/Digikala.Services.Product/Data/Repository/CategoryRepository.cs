using Digikala.Services.Product.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly Digikalaproduct _db;

        public CategoryRepository(Digikalaproduct db)
        {
            _db = db;
        }

        public async void addCategory(Category category)
        {
           await _db.categories.AddAsync(category);
            _db.SaveChanges();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var mycategories = await _db.categories.Select(x => x).ToListAsync();
            return mycategories;
        }

        public async Task<Category> GetCategory(int? categoryid)
        {
            var mycategory = await _db.categories.Where(x => x.ID == categoryid).Select(x => x).FirstOrDefaultAsync();
            return mycategory;
        }

        public async Task<Category> Getcategoryidbyname(string name)
        {
            var mycategory = await _db.categories.Where(x => x.CategoryName == name).Select(x => x).FirstOrDefaultAsync();
            return mycategory;
        }
    }
}
