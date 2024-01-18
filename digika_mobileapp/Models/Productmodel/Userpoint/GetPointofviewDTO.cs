using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digika_mobileapp.Models.Productmodel.Userpoint
{
    public class GetPointofviewDTO
    {
        public int id { get; set; }
        public string Commenttext { get; set; }
        public string Commenttitle { get; set; }
        public double Score { get; set; }
        public string[] Positivepoints { get; set; }
        public string[] Negativepoints { get; set; }
    }
}
