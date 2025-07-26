using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ChatParticipantConfiguration : IEntityTypeConfiguration<ChatParticipant>
{
    public void Configure(EntityTypeBuilder<ChatParticipant> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsAdmin).IsRequired();

        builder.HasIndex(x => new { x.ChatId, x.UserId }).IsUnique();

        builder.HasOne(x => x.Chat)
               .WithMany(c => c.Participants)
               .HasForeignKey(x => x.ChatId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
               .WithMany(u => u.Participants)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
