using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebMVC.Controllers;

public class AuthController(IMediator mediator) : Controller
{
    [HttpGet("Login")]
    public IActionResult Login() => View();

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromForm]LoginCommand command, [FromForm]bool rememberMe)
    {
        if (!ModelState.IsValid)
            return View(command);
        try
        {

            var response = await mediator.Send(command);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.Id.ToString()),
                new Claim(ClaimTypes.Name, response.UserName),
                new Claim("DisplayName", response.DisplayName),
                new Claim(ClaimTypes.Email, response.Email),
                new Claim("IsVerified", response.IsVerified.ToString()),
                new Claim("ProfileImageUrl", response.ProfileImageUrl ?? ""),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe ? DateTimeOffset.UtcNow.AddDays(30) : null
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            return RedirectToAction("Index", "Chat");
        }
        catch (Exception e)
        {
            ViewBag.ErrorMessage = e.Message;
            return View(command);
        }
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Chat");
    }


    [HttpGet("Register")]
    public IActionResult Register() => View();

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        if (!ModelState.IsValid)
            return View(command);


        try
        {
            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Login));
        }
        catch (Exception e)
        {
            ViewBag.ErrorMessage = e.Message;   
            return View(command);
        }
    }
}
