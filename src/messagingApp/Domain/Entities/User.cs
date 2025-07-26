using Core.Domain;


namespace Domain.Entities;

public class User : Entity<Guid>
{
    public string UserName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
    public bool IsVerified { get; set; }
    public string? ProfileImageUrl { get; set; }
}
