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
    public class ProductDetailEntityConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.HasKey(pd => pd.Id);

            builder.Property(pd => pd.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(pd => pd.Product)
                .WithMany(p => p.ProductDetail)
                .HasForeignKey(pd => pd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pd => pd.ProductDetailType)
                .WithMany(pdt => pdt.ProductDetails)
                .HasForeignKey(pd => pd.ProductDetailTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("ProductDetails");
        }
    }
}
