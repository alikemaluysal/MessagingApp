namespace WebMVC.Models;

public class UserChatViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ImageIdentifier { get; set; }
    public string? InvitationCode { get; set; }
    public string? LastMessage { get; set; }
    public DateTime? LastMessageDate { get; set; }
}

