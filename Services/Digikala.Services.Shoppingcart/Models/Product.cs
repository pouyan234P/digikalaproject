using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public byte[] Picture { get; set; }
    }
}
