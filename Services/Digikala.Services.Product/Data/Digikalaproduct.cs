using Digikala.Services.Product.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Data
{
    public class Digikalaproduct:DbContext
    {
        public Digikalaproduct(DbContextOptions<Digikalaproduct> options): base(options)
        {

        }
        public DbSet<Products> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Pointofview> pointofviews { get; set; }
        public DbSet<Questionandanswer> questionandanswers { get; set; }
        public DbSet<UserPoint> userPoints { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Subcategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.CategoryParent);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Categoryid)
                .HasForeignKey(p => p.myCategoryId);
        }
    }
}
