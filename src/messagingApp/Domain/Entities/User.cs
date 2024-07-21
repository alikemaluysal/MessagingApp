using Core.Persistence.Domain;

namespace Domain.Entities;

public class User : Entity<Guid>
{
    public string Nickname { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string? ProfileImageIdentifier { get; set; }
    public bool IsVerified { get; set; }
    public string? VerificationCode { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<ChatUser> ChatUsers { get; set; } = default!;
    public virtual ICollection<Message> Messages { get; set; } = default!;
    public virtual ICollection<MessageUserState> MessageUserStates { get; set; } = default!;
    public virtual ICollection<UserRole> UserRoles { get; set; } = default!;

}
