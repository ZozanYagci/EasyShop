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
    public class AuthUserOperationClaimEntityConfiguration : IEntityTypeConfiguration<AuthUserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<AuthUserOperationClaim> builder)
        {
            builder.HasKey(uoc => uoc.Id);

            builder.HasOne(uc => uc.AuthUser)
                  .WithMany(u => u.AuthUserOperationClaims)
                  .HasForeignKey(uc => uc.AuthUserId)
                  .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(uc => uc.OperationClaim)
                   .WithMany(o => o.AuthUserOperationClaims)
                   .HasForeignKey(uc => uc.OperationClaimId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(uc => new { uc.AuthUserId, uc.OperationClaimId })
                   .IsUnique();

            builder.ToTable("AuthUserOperationClaims");
        }
    }
}
