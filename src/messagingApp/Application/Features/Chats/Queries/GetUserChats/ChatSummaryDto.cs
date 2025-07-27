namespace Application.Features.Chats.Queries.GetUserChats;

public class ChatSummaryDto
{
    public Guid ChatId { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsGroup { get; set; }
    public DateTime? LastMessageTime { get; set; }
}
