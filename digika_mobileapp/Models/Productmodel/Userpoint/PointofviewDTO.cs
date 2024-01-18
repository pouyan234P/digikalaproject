using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Models.Productmodel.Userpoint
{
    public class PointofviewDTO
    {
        public int id { get; set; }
        public ProductDTO Productid { get; set; }
        public string Commenttext { get; set; }
        public string Commenttitle { get; set; }
        public double Score { get; set; }
        public byte[] Positivepoints { get; set; }
        public byte[] Negativepoints { get; set; }
    }
}
