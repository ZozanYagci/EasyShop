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
    public class ProductAttributeEntityConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {

            builder.HasKey(pa => pa.Id);


            builder.HasIndex(pa => new { pa.ProductId, pa.AttributeValueId })
                .IsUnique();


            builder.HasOne(pa => pa.Product)
                .WithMany(p => p.ProductAttributes)
                .HasForeignKey(pa => pa.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(pa => pa.AttributeValue)
                .WithMany(a => a.ProductAttributes)
                .HasForeignKey(pa => pa.AttributeValueId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.ToTable("ProductAttributes");

        }
    }
}