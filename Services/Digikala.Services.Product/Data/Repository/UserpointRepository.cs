using Digikala.Services.Product.Helper;
using Digikala.Services.Product.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
    public class UserpointRepository : IUserpointRepository
    {
        private readonly Digikalaproduct _db;

        public UserpointRepository(Digikalaproduct db)
        {
            _db = db;
        }
        public async Task AddUserpoint(UserPoint userPoint)
        {
             await _db.pointofviews.AddAsync(userPoint.Pointofiviewid);
             _db.SaveChanges();
            var lastpointofview = await _db.pointofviews.OrderBy(x => x).LastOrDefaultAsync();
            userPoint.Pointofiviewid.id = lastpointofview.id;
            var myproduct =await _db.products.Where(x => x.id == userPoint.Productid.id).Select(x => x).FirstOrDefaultAsync();
            userPoint.Productid = myproduct;
            await _db.userPoints.AddAsync(userPoint);
             _db.SaveChanges();
        }

        public async Task<UserPoint> GetUserPoint(int id)
        {
            var myuserpoint = await _db.userPoints.Where(x => x.id == id).Select(x => x).Include(t => t.Pointofiviewid).Include(t => t.Productid).FirstOrDefaultAsync();
            return myuserpoint;
        }

        public async Task<PagedList<UserPoint>> GetUserPoints(int productid,UserParams userParams)
        {
            var myuserpoint =  _db.userPoints.Where(x => x.Productid.id == productid).Include(x => x.Pointofiviewid).Include(x => x.Productid).ThenInclude(x=>x.Categoryid);
            return await PagedList<UserPoint>.CreateAsynce(myuserpoint, userParams.PageNumber, userParams.pageSize);
        }
    }
}
