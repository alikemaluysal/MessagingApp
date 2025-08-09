using Microsoft.AspNetCore.SignalR;

namespace WebMVC.Hubs;

public class ChatHub : Hub
{
    public async Task JoinChat(Guid chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}
