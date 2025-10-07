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
            builder.Property(sc => sc.AuthUserId).IsRequired();

            builder.Property(sc => sc.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(sc => sc.AuthUser)
                   .WithMany()
                   .HasForeignKey(sc => sc.AuthUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(sc => sc.Items)
                   .WithOne(i => i.Cart)
                   .HasForeignKey(i => i.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Table Mapping
            builder.ToTable("ShoppingCarts");
        }
    }
}
