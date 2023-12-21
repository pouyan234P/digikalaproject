using Digikala.Services.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
   public interface IQuestionandanswerRepository
    {
       public void Addquestionandanswer(Questionandanswer questionandanswer);
        public Task<IEnumerable<Questionandanswer>> GetQuestionandanswers(int productid);
    }
}
