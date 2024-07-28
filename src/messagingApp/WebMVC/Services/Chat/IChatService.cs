using WebMVC.Models;

namespace WebMVC.Services.Chat;

public interface IChatService
{
    Task<List<UserChatViewModel>> GetUserChats(Guid userId);
    Task<ChatDetailViewModel> GetChatDetails(Guid chatId);
    Task CreateGroupAsync(string groupName);
    Task JoinGroupAsync(string code, Guid userId);

}


