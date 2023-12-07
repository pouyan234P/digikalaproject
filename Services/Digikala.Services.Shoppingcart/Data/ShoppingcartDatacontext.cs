using Digikala.Services.Shoppingcart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Shoppingcart.Data
{
    public class ShoppingcartDatacontext:DbContext
    {
        public ShoppingcartDatacontext(DbContextOptions<ShoppingcartDatacontext> options): base(options)
        {

        }
        public DbSet<Cartdetail> cartdetails { get; set; }
        public DbSet<CartHeader> cartHeaders { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
