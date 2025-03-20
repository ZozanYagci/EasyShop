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
    public class AuthUserEntityConfiguration : IEntityTypeConfiguration<AuthUser>
    {
        public void Configure(EntityTypeBuilder<AuthUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Phone)
                 .HasMaxLength(15)
                 .IsRequired(false);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.PasswordSalt)
                .IsRequired();

            builder.Property(u => u.Status)
                .IsRequired();

            builder.HasMany(u => u.AuthUserOperationClaims)
                .WithOne(uoc => uoc.AuthUser)
                .HasForeignKey(uoc => uoc.AuthUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("AuthUsers");

        }
    }
}
