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
    public class ProductDetailTypeEntityConfiguration : IEntityTypeConfiguration<ProductDetailType>
    {
        public void Configure(EntityTypeBuilder<ProductDetailType> builder)
        {
            builder.HasKey(pdt => pdt.Id);

            builder.Property(pdt => pdt.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.ToTable("ProductDetailTypes");
        }
    }
}
