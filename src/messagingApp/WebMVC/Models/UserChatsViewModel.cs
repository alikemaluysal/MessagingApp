using Application.Features.Chats.Queries.GetUserChats;
using Application.Features.Messages.Queries.GetChatMessages;

namespace WebMVC.Models;

public class UserChatsViewModel
{
    public GetUserChatsResponse GetUserChatsResponse { get; set; }
    public GetChatMessagesResponse GetChatMessagesResponse { get; set; }
}
