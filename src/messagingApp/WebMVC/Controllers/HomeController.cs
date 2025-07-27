using System.Diagnostics;
using System.Security.Claims;
using Application.Features.Chats.Queries.GetUserChats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
