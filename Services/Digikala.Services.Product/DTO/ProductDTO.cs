using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.DTO
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string Informationid { get; set; }
        public CategoryDTO Categoryid { get; set; }
        public string Name { get; set; }
        public string Insurance { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public byte mainpicture { get; set; }
        public byte[] pictures { get; set; }
        public string Nameforushghah { get; set; }
    }
}
