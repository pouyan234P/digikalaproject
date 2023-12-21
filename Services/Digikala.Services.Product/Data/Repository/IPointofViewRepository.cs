using Digikala.Services.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
    public interface IPointofViewRepository
    {
        public void addpointofview(Pointofview pointofview);
        public Task<IEnumerable<Pointofview>> GetPointofviewsbyproduct(int productid);
        public Task<Pointofview> GetPointofviewbyid(int id);
    }
}
