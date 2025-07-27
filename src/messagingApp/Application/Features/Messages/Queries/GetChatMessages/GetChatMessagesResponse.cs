namespace Application.Features.Messages.Queries.GetChatMessages;

public class GetChatMessagesResponse
{
    public List<MessageDto> Messages { get; set; } = new();
    public ChatInfoDto ChatInfo { get; set; } = new();
}
