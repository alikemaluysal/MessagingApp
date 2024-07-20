using Core.Persistence.Domain;

namespace Domain.Entities;

public class ChatUser : Entity<Guid>
{
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public virtual Chat Chat { get; set; } = default!;
    public virtual User User { get; set; } = default!;
}
