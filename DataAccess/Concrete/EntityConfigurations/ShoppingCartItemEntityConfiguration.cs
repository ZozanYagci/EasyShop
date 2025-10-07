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
    public class ShoppingCartItemEntityConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(i => i.Quantity)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(i => i.AddedAt)
                .IsRequired().HasDefaultValueSql("GETDATE()");

            builder.HasOne(i => i.Product)
                .WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("ShoppingCartItems");
        }
    }
}
