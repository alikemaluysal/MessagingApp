using Core.Domain;


namespace Domain.Entities;

public class ChatParticipant : Entity<Guid>
{
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public bool IsAdmin { get; set; }
    public virtual Chat Chat { get; set; } = default!;
    public virtual User User { get; set; } = default!;
}
