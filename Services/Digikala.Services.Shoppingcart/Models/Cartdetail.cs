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
        public int Headerid { get; set; }
        public int productid { get; set; }
        public int Count { get; set; }
    }
}
