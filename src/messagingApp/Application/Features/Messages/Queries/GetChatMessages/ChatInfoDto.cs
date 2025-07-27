namespace Application.Features.Messages.Queries.GetChatMessages;

public class ChatInfoDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public bool IsGroup { get; set; }
    public int ParticipantsCount { get; set; }
}