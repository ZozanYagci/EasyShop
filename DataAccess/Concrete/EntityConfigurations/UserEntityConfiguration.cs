using Core.Enums;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            // Properties
            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.Property(u => u.PasswordHash)
                     .IsRequired();

            builder.Property(u => u.PasswordSalt)
                   .IsRequired();


            builder.Property(u => u.Phone)
                   .HasMaxLength(20);

            builder.Property(u => u.Address)
                   .HasMaxLength(200);

            builder.Property(u => u.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.Status)
                .IsRequired()
                .HasDefaultValue(UserStatus.Active)
                .HasConversion<int>();


            // Relationships
            builder.HasMany(u => u.Orders)
                   .WithOne(o => o.User)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Reviews)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(u => u.ShoppingCarts)
            //       .WithOne(sc => sc.User)
            //       .HasForeignKey(sc => sc.UserId)
            //       .OnDelete(DeleteBehavior.Restrict);  

            // Table Mapping
            builder.ToTable("Users");
        }
    }
}
