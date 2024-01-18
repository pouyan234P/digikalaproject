using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Models.Productmodel
{
    public class GetUserPointDTO
    {
        public int id { get; set; }
        public GetPointofviewDTO Pointofiviewid { get; set; }
        public int Userid { get; set; }
        public ProductDTO Productid { get; set; }
    }
}
