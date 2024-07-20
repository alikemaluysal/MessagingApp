using Core.Persistence.Domain;

namespace Domain.Entities;

public class Message : Entity<Guid>
{
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public string? Content { get; set; }
    public string? FileIdentifier { get; set; }
    public DateTime SentAt { get; set; }

    public virtual Chat Chat { get; set; } = default!;
    public virtual User User { get; set; } = default!;
    public virtual ICollection<MessageUserState> MessageUserStates { get; set; } = default!;
}
