using Digikala.Services.Payment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Payment.Data
{
    public class PaymentDatacontext:DbContext
    {
        public PaymentDatacontext(DbContextOptions<PaymentDatacontext> options): base(options)
        {

        }
        public DbSet<Payments>  payments { get; set; }
    }
}
