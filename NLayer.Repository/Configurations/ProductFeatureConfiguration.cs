using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.Product).WithOne(x => x.ProductFeature).HasForeignKey<ProductFeature>(x => x.ProductId);//bir Product bir ProductFeature  ile ilişki içinde olabilir. O yüzden WithOne ile yazdık.Ve ForeignKey de burada
                                                                                                                          //burada belirtilebilir. Fakat Entity'lerde doğru bir şekilde belirttiğimiz için EF bunu doğru bir şekilde yakalayabiliyor.




        }
    }
}
