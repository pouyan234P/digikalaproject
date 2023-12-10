using Digikala.Services.Checkout.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Checkout.Data
{
    public class digikalacheckout:DbContext
    {
        public digikalacheckout(DbContextOptions<digikalacheckout> options): base(options)
        {

        }
        DbSet<Summary> summaries { get; set; }
    }
}
