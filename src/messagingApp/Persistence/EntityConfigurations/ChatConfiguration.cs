using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.IsGroup).IsRequired();

        builder.HasOne(x => x.CreatedBy)
               .WithMany(u => u.CreatedChats)
               .HasForeignKey(x => x.CreatedById)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Messages)
               .WithOne(m => m.Chat)
               .HasForeignKey(m => m.ChatId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Participants)
               .WithOne(p => p.Chat)
               .HasForeignKey(p => p.ChatId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
