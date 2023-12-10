using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Models
{
    public class Cartdetail
    {
        [Key]
        public int id { get; set; }
        public CartHeader Headerid { get; set; }
        public Product productid { get; set; }
        public int Count { get; set; }
    }
}
