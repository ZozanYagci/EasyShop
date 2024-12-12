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
    public class ShoppingCartEntityConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(sc => sc.Id);

            // Properties
            builder.Property(sc => sc.Quantity)
                   .IsRequired(); 

            builder.Property(sc => sc.AddedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()"); 

            // Relationships
            builder.HasOne(sc => sc.User)
                   .WithMany(u => u.ShoppingCarts)
                   .HasForeignKey(sc => sc.UserId)
                   .OnDelete(DeleteBehavior.Restrict);  

            builder.HasOne(sc => sc.Product)
                   .WithMany(p => p.ShoppingCarts)
                   .HasForeignKey(sc => sc.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);  

            // Table Mapping
            builder.ToTable("ShoppingCarts");
        }
    }
}
