using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Models
{
    public class Pointofview
    {
        public int id { get; set; }
        public string Commenttext { get; set; }
        public string Commenttitle { get; set; }
        public double Score { get; set; }
        public byte[] Positivepoints { get; set; }
        public byte[] Negativepoints { get; set; }
    }
}
