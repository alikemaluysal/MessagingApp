using WebMVC.Models;

namespace WebMVC.Services.Message;

public interface IMessageService
{
    Task<List<ChatMessageViewModel>> GetChatMessagesAsync(Guid chatId);
    Task<ChatMessageViewModel> SendMessageAsync(ChatMessageViewModel message);
}
