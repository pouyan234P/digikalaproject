using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.DTO
{
    public class QuestionandanswerDTO
    {
        public int id { get; set; }
        public string Question { get; set; }
        public string answer { get; set; }
        public int Userquestionid { get; set; }
        public int Useranswerid { get; set; }
        public ProductDTO Productid { get; set; }
    }
}
