using Digikala.Services.Product.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data.Repository
{
    public class PointofViewRepository : IPointofViewRepository
    {
        private readonly Digikalaproduct _db;

        public PointofViewRepository(Digikalaproduct db)
        {
            _db = db;
        }
        public async void addpointofview(UserPoint userPoint)
        {
            await _db.pointofviews.AddAsync(userPoint.Pointofiviewid);
            var mypointofview = await _db.pointofviews.Select(x => x).LastOrDefaultAsync();
            userPoint.Pointofiviewid = mypointofview;
            await _db.userPoints.AddAsync(userPoint);
            _db.SaveChanges();
        }

        public async Task<Pointofview> GetPointofviewbyid(int id)
        {
            var mypointofview = await _db.pointofviews.Where(x => x.id == id).Select(x => x).FirstOrDefaultAsync();
            return mypointofview;
        }

        public async Task<IEnumerable<UserPoint>> GetPointofviewsbyproduct(int productid)
        {
            var mypointofview = await _db.userPoints.Where(x => x.Productid.id == productid).Select(x => x).ToListAsync();
            return mypointofview;
        }
    }
}
