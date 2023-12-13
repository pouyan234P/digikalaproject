using Digikala.Services.Checkout.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Checkout.Data.Repository
{
    public class SummaryRepository : ISummaryRepository
    {
        private readonly digikalacheckout _db;

        public SummaryRepository(digikalacheckout db)
        {
            _db = db;
        }
        public async Task<Summary> Addsummary(Summary summary)
        {
            await _db.summaries.AddAsync(summary);
            _db.SaveChanges();
            var mysummary = await _db.summaries.Select(x => x).LastOrDefaultAsync();
            return mysummary;
        }

        public async Task<Summary> GetSummary(int userid)
        {
            var mysummary = await _db.summaries.Where(x => x.Userid == userid).Select(x => x).FirstOrDefaultAsync();
            return mysummary;
        }
    }
}
