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
    public class AttributeValueEntityConfiguration : IEntityTypeConfiguration<AttributeValue>
    {
        public void Configure(EntityTypeBuilder<AttributeValue> builder)
        {
            builder.HasKey(av => av.Id);

            builder.Property(av => av.Value)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(av => av.Attribute)
                .WithMany(a => a.AttributeValues)
                .HasForeignKey(av => av.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(av => av.ProductAttributes)
                .WithOne(pa => pa.AttributeValue)
                .HasForeignKey(pa => pa.AttributeValueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("AttributeValues");
        }
    }
}