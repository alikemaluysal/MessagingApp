namespace Application.Features.Chats.Queries.GetUserChats;

public class GetUserChatsResponse
{
    public List<ChatSummaryDto> DirectMessages { get; set; }
    public List<ChatSummaryDto> Channels { get; set; }
}
