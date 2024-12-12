using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityConfigurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);  //primary key


            //property
            builder.Property(c => c.Name) 
                .IsRequired()  
                .HasMaxLength(250);

            builder.Property(c => c.Description)
                .HasMaxLength(500);


            //table Mapping

            builder.ToTable("Categories");
        }
    }
}
