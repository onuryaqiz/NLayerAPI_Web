using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //Options ile veritabanı Startup dosyasından verebilmek için DbContextOptions veriyoruz.
        {


        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductFeature> ProductFeature { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//IEntityTypeConfiguration Interface'e sahip olan class'ları bulup Reflection yaparak class'ları buluyor.
            //modelBuilder.ApplyConfiguration(new ProductConfiguration()) ile yazabiliriz. Fakat birçok assembly yani class library için bunu yapmak yerine 
            //yukarıdaki metod ile bu sorunu çözebiliriz.
            base.OnModelCreating(modelBuilder);
        }

    }
}
