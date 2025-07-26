using Core.Domain;

namespace Domain.Entities;

public class Chat : Entity<Guid>
{
    public string? Name { get; set; }
    public string? GroupImageUrl { get; set; }
    public bool IsGroup { get; set; }
    public Guid CreatedById { get; set; }
    public User CreatedBy { get; set; } = default!;
}
