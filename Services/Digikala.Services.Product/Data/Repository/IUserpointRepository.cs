using Digikala.Services.Product.Helper;
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
        Task<PagedList<UserPoint>> GetUserPoints(int productid,UserParams userParams);
        Task<UserPoint> GetUserPoint(int id);
    }
}
