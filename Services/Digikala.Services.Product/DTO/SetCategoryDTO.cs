using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.DTO
{
    public class SetCategoryDTO
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public int catparentid { get; set; }
    }
}
