namespace Core.Domain;

public class Entity<TId>
{
    public TId Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
