using Application.Features.Chats.Queries.GetUserChats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebMVC.Controllers;

[Authorize]
public class ChatController(IMediator mediator) : Controller
{
    public async Task<IActionResult> Index()
    {
        try
        {
            var query = new GetUserChatsQuery
            {
                UserId = getUserId()
            };


            GetUserChatsResponse response = await mediator.Send(query);
            return View(response);
        }
        catch (Exception)
        {

            throw;
        }

    }


    private Guid getUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userId, out var id) ? id : Guid.Empty;
    }
}
