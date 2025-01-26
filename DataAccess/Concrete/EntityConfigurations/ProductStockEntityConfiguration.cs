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
    public class ProductStockEntityConfiguration : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Color)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(ps => ps.Size)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ps => ps.StockQuantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(ps => ps.Product)
                 .WithMany(p => p.ProductStocks)
                 .HasForeignKey(ps => ps.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("ProductStocks");
        }
    }
}
