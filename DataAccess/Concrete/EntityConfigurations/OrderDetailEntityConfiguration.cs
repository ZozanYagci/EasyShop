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
    public class OrderDetailEntityConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => od.Id);

            // Properties
            builder.Property(od => od.Quantity)
                   .IsRequired()
                   .HasDefaultValue(1); 

            builder.Property(od => od.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Relationships
            builder.HasOne(od => od.Order)
                   .WithMany(o => o.Detail)
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);  // Sipariş silinirse, ilgili detaylar da silinir.

            builder.HasOne(od => od.Product)
                   .WithMany(p => p.OrderDetails)
                   .HasForeignKey(od => od.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);  // Ürün silinirse, sipariş detayları silinmez.

            // Table Mapping
            builder.ToTable("OrderDetails");

        }
    }
}
