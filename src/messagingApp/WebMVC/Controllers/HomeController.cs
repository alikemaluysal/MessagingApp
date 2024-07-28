using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMVC.Models;

namespace WebMVC.Controllers;

[Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }


    public IActionResult GetChatMessages()
    {
        return PartialView("_ChatMessages");
    }

    public IActionResult GetUserChats()
    {
        return PartialView("_UserChats");
    }
}
