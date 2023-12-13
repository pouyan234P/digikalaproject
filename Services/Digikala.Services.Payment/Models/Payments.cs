using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Payment.Models
{
    public class Payments
    {
        public int id { get; set; }
        public string Bank { get; set; }
        public bool Status { get; set; }
        public int Checkoutid { get; set; }
    }
}
