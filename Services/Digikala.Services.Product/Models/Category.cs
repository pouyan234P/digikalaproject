using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryParent { get; set; }
        public List<Products> Products { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> Subcategories { get; set; }
    }
}
