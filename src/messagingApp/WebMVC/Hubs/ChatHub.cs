using Microsoft.AspNetCore.SignalR;

namespace WebMVC.Hubs;

public class ChatHub : Hub
{
    //public async Task SendMessage(Guid chatId, Guid senderId, string content)
    //{
    //    await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", chatId, senderId, content);
    //}

    public async Task JoinChat(Guid chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}
