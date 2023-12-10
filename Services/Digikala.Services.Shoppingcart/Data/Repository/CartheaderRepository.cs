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
            await _db.cartHeaders.AddAsync(cartHeader);
            var mycartheader = await _db.cartHeaders.Select(x => x).LastOrDefaultAsync();
            return mycartheader;
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
