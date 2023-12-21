using Digikala.Services.Product.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
    public class QuestionandanswerRepository : IQuestionandanswerRepository
    {
        private readonly Digikalaproduct _db;

        public QuestionandanswerRepository(Digikalaproduct db)
        {
            _db = db;
        }
        public async void Addquestionandanswer(Questionandanswer questionandanswer)
        {
            await _db.questionandanswers.AddAsync(questionandanswer);
            _db.SaveChanges();
        }

        public async Task<IEnumerable<Questionandanswer>> GetQuestionandanswers(int productid)
        {
            var myquestionandanswer = await _db.questionandanswers.Where(x => x.Productid.id == productid).Select(x => x).ToListAsync();
            return myquestionandanswer;
        }
    }
}
