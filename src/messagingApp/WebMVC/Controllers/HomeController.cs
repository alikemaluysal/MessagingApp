using System.Diagnostics;
using Messaging.Models;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
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

}
