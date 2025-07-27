using System.Diagnostics;
using System.Security.Claims;
using Application.Features.Chats.Queries.GetUserChats;
using MediatR;
using Messaging.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Controllers;

[Authorize]
public class HomeController(IMediator mediator) : Controller
{
    public async Task<IActionResult> IndexAsync()
    {
        try
        {
            var query = new GetUserChatsQuery
            {
                UserId = getUserId()
            };

            var response = await mediator.Send(query);
        }
        catch (Exception)
        {

            throw;
        }

        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Twostep()
    {
        return View();
    }


    private Guid getUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userId, out var id) ? id : Guid.Empty;
    }

}
