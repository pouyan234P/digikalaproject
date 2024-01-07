using Digikala.Services.Shoppingcart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Data.Repository
{
    public class CartheaderRepository : ICartheaderRepository
    {
        private readonly ShoppingcartDatacontext _db;

        public CartheaderRepository(ShoppingcartDatacontext db)
        {
            _db = db;
        }
        public async Task<CartHeader> AddCartheader(CartHeader cartHeader)
        {
            var mycart = await _db.cartHeaders.Where(x => x.Userid == cartHeader.Userid).Select(x => x).FirstOrDefaultAsync();
            if (mycart == null)
            {
                await _db.cartHeaders.AddAsync(cartHeader);
                _db.SaveChanges();
                var mycartheader = await _db.cartHeaders.OrderBy(x=>x).LastOrDefaultAsync();
                return mycartheader;
            }

            return mycart;
        }

        public async Task<IEnumerable<CartHeader>> GetAllCartheader()
        {
            var cartheaders = await _db.cartHeaders.Select(x => x).ToListAsync();
            return cartheaders;
        }

        public async Task<CartHeader> GetCartHeader(int userid)
        {
            var cartheader = await _db.cartHeaders.Where(x => x.Userid == userid).Select(x => x).FirstOrDefaultAsync();
            return cartheader;
        }
    }
}
