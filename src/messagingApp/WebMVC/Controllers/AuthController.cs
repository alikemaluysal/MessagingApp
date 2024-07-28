using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services.Auth;
using WebMVC.Services.Token;

namespace WebMVC.Controllers;

public class AuthController(
    IAuthService authService,
    ITokenService tokenService
    ) : Controller
{
    [HttpPost("/Login")]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
            return View(loginViewModel);

        var token = await authService.LoginAsync(loginViewModel.Email, loginViewModel.Password);
        tokenService.SetAccessToken(token.AccessToken);
        tokenService.SetRefreshToken(token.RefreshToken);

        TempData["SuccessMessage"] = "Login successsfully.";

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("/Login")]
    public async Task<IActionResult> Login()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Verify()
    {
        return View();
    }


}
