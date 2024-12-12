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
    public class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.PaymentDate)
                   .IsRequired(); 

            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");  

            builder.Property(p => p.PaymentMethod)
                   .IsRequired()
                   .HasMaxLength(50); 

            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasMaxLength(50);  

            // Relationships
            builder.HasOne(p => p.Order)
                   .WithMany(o => o.Payments)
                   .HasForeignKey(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);  // Bir ödeme kaydı silindiğinde, sipariş kaydı silinmemeli

            // Table Mapping
            builder.ToTable("Payments");
        }
    }
}
