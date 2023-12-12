using Digikala.Services.Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Checkout.Data.Repository
{
   public interface ISummaryRepository
    {
        Task<Summary> Addsummary(Summary summary);
        Task<Summary> GetSummary(int userid);
    }
}
