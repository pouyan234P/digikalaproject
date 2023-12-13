using Digikala.Services.Payment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Payment.Data.Repository
{
   public interface IPaymentsRepository
    {
        Task<Payments> AddPayments(Payments payments);
        Task<Payments> GetPayments(int id);
        Task<IEnumerable<Payments>> GetPaymentsbyuserid(int userid);
        Task<IEnumerable<Payments>> GetallPayments();
    }
}
