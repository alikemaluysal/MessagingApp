using Core.Application.Security;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
        builder.Property(u => u.Nickname).HasColumnName("Nickname").IsRequired().HasMaxLength(50);
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired().HasMaxLength(255);
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.ProfileImageIdentifier).HasColumnName("ProfileImageIdentifier").HasMaxLength(255);
        builder.Property(u => u.IsVerified).HasColumnName("IsVerified").IsRequired();
        builder.Property(u => u.VerificationCode).HasColumnName("VerificationCode").HasMaxLength(100);
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasMany(u => u.RefreshTokens)
               .WithOne(rt => rt.User)
               .HasForeignKey(rt => rt.UserId);

        builder.HasMany(u => u.ChatUsers)
               .WithOne(cu => cu.User)
               .HasForeignKey(cu => cu.UserId);


        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash("1234", out passwordHash, out passwordSalt);
        //TODO: Add user seed
        builder.HasData(new User
        {
            Id = Guid.NewGuid(),
            Nickname = "Admin",
            Email = "admin@mail.com",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        });
    }
}
