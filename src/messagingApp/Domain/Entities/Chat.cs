using Core.Persistence.Domain;

namespace Domain.Entities;

public class Chat : Entity<Guid>
{
    public string Name { get; set; }
    public string? ImageIdentifier { get; set; }
    public string? InvitationCode { get; set; }

    public virtual ICollection<ChatUser> ChatUsers { get; set; } = default!;
    public virtual ICollection<Message> Messages { get; set; } = default!;

}
