using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using WebMVC.Models;
using WebMVC.Services.Message;

namespace WebMVC.Hubs;

public class ChatHub(IMessageService messageService) : Hub
{
    public async Task SendMessage(string messageJson)
    {
        //TODO: handle exceptions

        var message = JsonSerializer.Deserialize<ChatMessageViewModel>(messageJson,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        await messageService.SendMessageAsync(message);

        await Clients.All.SendAsync("ReceiveMessage", messageJson);
    }
}
