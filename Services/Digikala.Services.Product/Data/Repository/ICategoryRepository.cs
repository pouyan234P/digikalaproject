using Digikala.Services.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
   public interface ICategoryRepository
    {
        void addCategory(Category category);
        Task<Category> GetCategory(int categoryid);
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> Getcategoryidbyname(string name);
    }
}
