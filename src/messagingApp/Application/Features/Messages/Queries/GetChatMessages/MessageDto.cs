namespace Application.Features.Messages.Queries.GetChatMessages;

public class MessageDto
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public bool IsSender { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string? SenderImageUrl { get; set; } = string.Empty;
}
