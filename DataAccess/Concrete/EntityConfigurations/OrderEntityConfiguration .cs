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
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); 

            builder.Property(o => o.ShippingAddress)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(o => o.Status)
            .IsRequired()
            .HasMaxLength(50);

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Detail)
        .WithOne(d => d.Order)
        .HasForeignKey(d => d.OrderId)
        .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(o => o.Payments)
                   .WithOne(p => p.Order)
                   .HasForeignKey(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);  // Sipariş iptal edilirse, ilgili ödeme kayıtları da otomatik silinmeli.

            // Table Mapping
            builder.ToTable("Orders");


        }
    }
}
