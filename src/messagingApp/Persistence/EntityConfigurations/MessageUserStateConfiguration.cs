using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MessageUserStateConfiguration : IEntityTypeConfiguration<MessageUserState>
{
    public void Configure(EntityTypeBuilder<MessageUserState> builder)
    {
        builder.ToTable("MessageUserStates").HasKey(mus => mus.Id);

        builder.Property(mus => mus.Id).HasColumnName("Id").IsRequired();
        builder.Property(mus => mus.MessageId).HasColumnName("MessageId").IsRequired();
        builder.Property(mus => mus.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(mus => mus.ReadAt).HasColumnName("ReadAt");
        builder.Property(mus => mus.DeliveredAt).HasColumnName("DeliveredAt");
        builder.Property(mus => mus.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(mus => mus.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(mus => mus.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(mus => !mus.DeletedDate.HasValue);

        builder.HasOne(mus => mus.Message)
               .WithMany(m => m.MessageUserStates)
               .HasForeignKey(mus => mus.MessageId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(mus => mus.User)
               .WithMany(u => u.MessageUserStates)
               .HasForeignKey(mus => mus.UserId).OnDelete(DeleteBehavior.NoAction);
    }
}
