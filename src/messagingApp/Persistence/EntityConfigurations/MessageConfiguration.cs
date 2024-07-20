using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages").HasKey(m => m.Id);

        builder.Property(m => m.Id).HasColumnName("Id").IsRequired();
        builder.Property(m => m.ChatId).HasColumnName("ChatId").IsRequired();
        builder.Property(m => m.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(m => m.Content).HasColumnName("Content").HasMaxLength(1000);
        builder.Property(m => m.FileIdentifier).HasColumnName("FileIdentifier").HasMaxLength(255);
        builder.Property(m => m.SentAt).HasColumnName("SentAt").IsRequired();
        builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(m => m.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(m => !m.DeletedDate.HasValue);

        builder.HasOne(m => m.Chat)
               .WithMany(c => c.Messages)
               .HasForeignKey(m => m.ChatId);
        builder.HasOne(m => m.User)
               .WithMany(u => u.Messages)
               .HasForeignKey(m => m.UserId);

        builder.HasMany(m => m.MessageUserStates)
               .WithOne(mus => mus.Message)
               .HasForeignKey(mus => mus.MessageId);
    }
}
