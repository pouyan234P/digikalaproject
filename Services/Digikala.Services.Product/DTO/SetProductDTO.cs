using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.DTO
{
    public class SetProductDTO
    {
        public IFormFile MainPictureUrl { get; set; }
        public IList<IFormFile> PictureUrl { get; set; }
        public CategoryDTO Categoryid { get; set; }
        public string Name { get; set; }
        public string Insurance { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Nameforushghah { get; set; }
        public object information { get; set; }
        public double averageScore { get; set; }
        public string hashColor { get; set; }
    }
}
