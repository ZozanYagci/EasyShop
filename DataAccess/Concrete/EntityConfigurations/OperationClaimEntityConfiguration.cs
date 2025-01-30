using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityConfigurations
{
    public class OperationClaimEntityConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.HasKey(oc => oc.Id);

            builder.Property(oc => oc.Name)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.HasMany(oc => oc.AuthUserOperationClaims)
                .WithOne(uoc => uoc.OperationClaim)
                .HasForeignKey(uoc => uoc.OperationClaimId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("OperationClaims");
        }
    }
}
