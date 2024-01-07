using Digikala.Services.Shoppingcart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Data.Repository
{
    public class CartdetialRepository : ICartdetailRepository
    {
        private readonly ShoppingcartDatacontext _db;

        public CartdetialRepository(ShoppingcartDatacontext db)
        {
            _db = db;
        }
        public async Task<Cartdetail> AddCartdetail(Cartdetail cartdetail)
        {
            await _db.cartdetails.AddAsync(cartdetail);
            _db.SaveChanges();
            var mycartdetail = await _db.cartdetails.OrderBy(x=>x).LastOrDefaultAsync();
            return mycartdetail;
        }

        public async Task<IEnumerable<Cartdetail>> GetAllCartdetail(int Userid)
        {
            var mycartdetail = await _db.cartdetails.Where(x => x.Headerid.Userid == Userid).Include(x=>x.productid).Include(x=>x.Headerid).ToListAsync();
            return mycartdetail;
        }

        public async Task<Cartdetail> GetCartdetail(int id)
        {
            var mycartdetail = await _db.cartdetails.Where(x => x.id == id).Select(x => x).FirstOrDefaultAsync();
            return mycartdetail;
        }
    }
}
