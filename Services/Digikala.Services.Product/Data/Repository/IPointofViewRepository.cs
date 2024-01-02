using Digikala.Services.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
    public interface IPointofViewRepository
    {
        public void addpointofview(UserPoint userPoint);
        public Task<IEnumerable<UserPoint>> GetPointofviewsbyproduct(int productid);
        public Task<Pointofview> GetPointofviewbyid(int id);
    }
}
