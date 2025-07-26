using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content)
               .IsRequired()
               .HasMaxLength(1000);

        builder.HasOne(x => x.Chat)
               .WithMany(c => c.Messages)
               .HasForeignKey(x => x.ChatId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Sender)
               .WithMany(u => u.SentMessages)
               .HasForeignKey(x => x.SenderId)
               .OnDelete(DeleteBehavior.Restrict);

    }
}
