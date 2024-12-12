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
    public class ReviewEntityConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            // Properties
            builder.Property(r => r.Rating)
                   .IsRequired()
                   .HasDefaultValue(1) 
                   .HasColumnType("int"); 

            builder.Property(r => r.Comment)
                   .HasMaxLength(500); 

            builder.Property(r => r.Date)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");  

            // Relationships
            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reviews)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Restrict);  // Kullanıcı silinirse, yorumlar silinmesin.

            builder.HasOne(r => r.Product)
                   .WithMany(p => p.Reviews)
                   .HasForeignKey(r => r.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);  // Ürün silinirse yorumlar da silinir

            // Table Mapping
            builder.ToTable("Reviews"); 

        }
    }
}
