using Application.Features.Chats.Queries.GetUserChats;
using Application.Features.Messages.Commands.SendMessage;
using Application.Features.Messages.Queries.GetChatMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMVC.Models;

namespace WebMVC.Controllers;

[Authorize]
public class ChatController(IMediator mediator) : Controller
{
    public async Task<IActionResult> Index([FromQuery] Guid selectedChatId)
    {

        var userId = getUserId();
        var getUserChatsQuery = new GetUserChatsQuery { UserId = userId };
        GetUserChatsResponse userChatsResponse = await mediator.Send(getUserChatsQuery);
        GetChatMessagesResponse messagesResponse = new();

        if (selectedChatId != Guid.Empty)
        {
            var getMessagesQuery = new GetChatMessagesQuery
            {
                UserId = userId,
                ChatId = selectedChatId
            };

            messagesResponse = await mediator.Send(getMessagesQuery);
        }


        var viewModel = new UserChatsViewModel
        {
            GetUserChatsResponse = userChatsResponse,
            GetChatMessagesResponse = messagesResponse
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
    {

        command.SenderId = getUserId();

        try
        {
            var response = await mediator.Send(command);
            return Ok(response);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }


    private Guid getUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userId, out var id) ? id : Guid.Empty;
    }
}
