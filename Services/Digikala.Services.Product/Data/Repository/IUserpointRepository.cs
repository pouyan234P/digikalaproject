using Digikala.Services.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
   public interface IUserpointRepository
    {
       Task AddUserpoint(UserPoint userPoint);
        Task<IEnumerable<UserPoint>> GetUserPoints(int productid);
        Task<UserPoint> GetUserPoint(int id);
    }
}
