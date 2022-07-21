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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id); //ID'si Key olacak.
            builder.Property(x => x.Id).UseIdentityColumn(); //1'er 1'er artan Identity belirliyoruz.
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50); //Max 50 karakter olabilir.

            builder.ToTable("Categories"); //Tablo ismi class ismi ne ise DB'deki adı belirliyoruz.


        }
    }
}
