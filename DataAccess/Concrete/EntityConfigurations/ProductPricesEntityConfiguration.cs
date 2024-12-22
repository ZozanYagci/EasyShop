using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityConfigurations
{
    public class ProductPricesEntityConfiguration : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.HasKey(pp => pp.Id); //primary key


            //property
            builder.Property(pp => pp.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(pp => pp.IsCurrent)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(pp => pp.EffectiveDate)
                .IsRequired()
                .HasColumnType("datetime");

            //Relationships
            builder.HasOne(pp => pp.Product)
                .WithMany(p => p.ProductPrices)
                .HasForeignKey(pp => pp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // database de Price değişikliklerini takip etmek için ekledim
            builder.HasIndex(pp => new { pp.ProductId, pp.IsCurrent })
                .HasFilter("[IsCurrent] = 1")
                .IsUnique();  // her ürüne sadece bir aktif fiyat olabilir.


            //table mapping
            builder.ToTable("ProductPrices");
        }
    }
}
