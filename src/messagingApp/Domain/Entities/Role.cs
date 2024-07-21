using Core.Persistence.Domain;

namespace Domain.Entities;

public class Role : Entity<Guid>
{
    public string Name { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
}