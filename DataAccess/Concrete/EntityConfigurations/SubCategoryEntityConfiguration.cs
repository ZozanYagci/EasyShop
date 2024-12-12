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
    public class SubCategoryEntityConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(c => c.Id);

            //property
            builder.Property(c => c.Name)
                .IsRequired()  
                .HasMaxLength(250);  

           // RelationShips
            builder.HasMany(c => c.Products) 
                .WithOne(c => c.SubCategory)  
                .HasForeignKey(p => p.SubCategoryId) 
                .OnDelete(DeleteBehavior.Restrict); 

            //table Mapping

            builder.ToTable("SubCategory"); 
        }
    }
}