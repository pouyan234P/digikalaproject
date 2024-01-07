using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Models.Shoppingmodel
{
    public class CartDetailDTO
    {
        public int id { get; set; }
        public CartHeaderDTO Headerid { get; set; }
        public ProductDTO productid { get; set; }
        public int Count { get; set; }
    }
}
