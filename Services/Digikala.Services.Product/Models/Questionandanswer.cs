using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Models
{
    public class Questionandanswer
    {
        public int id { get; set; }
        public string Question { get; set; }
        public string answer { get; set; }
        public int Userquestionid { get; set; }
        public int Useranswerid { get; set; }
        public Products Productid { get; set; }
    }
}
