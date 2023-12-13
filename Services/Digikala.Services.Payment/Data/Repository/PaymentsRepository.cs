using Digikala.Services.Payment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Payment.Data.Repository
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly PaymentDatacontext _db;

        public PaymentsRepository(PaymentDatacontext db)
        {
            _db = db;
        }

        public async Task<Payments> AddPayments(Payments payments)
        {
            await _db.payments.AddAsync(payments);
            _db.SaveChanges();
            var mypayments = await _db.payments.Select(x => x).LastOrDefaultAsync();
            return mypayments;
        }

        public async Task<IEnumerable<Payments>> GetallPayments()
        {
            var mypayments = await _db.payments.Select(x => x).ToListAsync();
            return mypayments;
        }

        public async Task<Payments> GetPayments(int id)
        {
            var mypayments = await _db.payments.Where(x => x.id == id).Select(x => x).FirstOrDefaultAsync();
            return mypayments;
        }

        public async Task<IEnumerable<Payments>> GetPaymentsbyuserid(int userid)
        {
            var mypayments = await _db.payments.Where(x => x.Userid == userid).Select(x => x).ToListAsync();
            return mypayments;
        }
    }
}
