using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Diagnostics;
using System.Security.Claims;
using WebMVC.Models;
using WebMVC.Services.Chat;
using WebMVC.Services.Message;

namespace WebMVC.Controllers;

[Authorize]
public class HomeController(
    IChatService chatService,
    IMessageService messageService
    ): Controller
{
    public async Task<IActionResult> IndexAsync()
    {
        HomeViewModel model = new();
        model.UserChats = await chatService.GetUserChats(GetCurrentUserId());

        return View(model);
    }

    public async Task<IActionResult> GetChatMessages(Guid chatId)
    {
        var userId = GetCurrentUserId();
        var chatMessages = await messageService.GetChatMessagesAsync(chatId);
        chatMessages.ForEach(m => m.IsCurrentUser = m.UserId == userId);

        return PartialView("_ChatMessages", chatMessages);
    }

    public async Task<IActionResult> GetUserChatsAsync()
    {
        var userId = GetCurrentUserId();
        var chats = await chatService.GetUserChats(userId);
        return PartialView("_UserChats", chats);
    }


    private Guid GetCurrentUserId()
    {
        return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
