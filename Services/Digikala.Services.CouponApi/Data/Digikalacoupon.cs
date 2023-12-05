using Digikala.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.CouponApi.Data
{
    public class Digikalacoupon:DbContext
    {
        public Digikalacoupon(DbContextOptions<Digikalacoupon> options): base(options)
        {

        }
        public DbSet<digicoupon> digicoupons { get; set; }
        public DbSet<giftcard> giftcards { get; set; }
    }
}
