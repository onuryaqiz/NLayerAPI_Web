using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

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
            //                                                                              modelBuilder.ApplyConfiguration(new ProductConfiguration()) ile yazabiliriz. Fakat birçok assembly yani class library için bunu yapmak yerine 
            //                                                                              yukarıdaki metod ile bu sorunu çözebiliriz.

            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 100,
                Width = 200,
                ProductId = 1

            },
            new ProductFeature()
            {
                Id = 2,
                Color = "Mavi",
                Height = 300,
                Width = 500,
                ProductId = 2

            }

            );

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) // EFCore SaveChanges'a kadar entity'leri memory'de Track ediyor .SaveChanges metodu çağırınca DB'de yansıtıyor.
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false; // UpdatedDate alanında güncelleme yapılınca , EFCore'a CreatedDate alanında herhangi bir müdahalede bulunma demek istedik.

                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }


                    }
                }
            }



            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }


                    }
                }
            }

            return base.SaveChanges();
        }

    }
}
