namespace Application.Features.Chats.Commands.Join;

public class JoinedChatResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageIdentifier { get; set; }
    public string? InvitationCode { get; set; }
}

