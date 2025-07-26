using Core.Domain;

namespace Domain.Entities;

public class Message : Entity<Guid>
{
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;

    public virtual Chat Chat { get; set; } = default!;
    public virtual User Sender { get; set; } = default!;
}
