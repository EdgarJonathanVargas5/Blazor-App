using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechNotes.Infrastructure.Authentication;

namespace TechNotes.Controllers;

[Route("account")]
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("external-login")]
    public IActionResult ExternalLogin(string provider)
    {
        var redirectUrl = Url.Action(nameof(HandleExternalCallBack));
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    [AllowAnonymous]
    [HttpGet("external-callback")]
    public async Task<IActionResult> HandleExternalCallBack()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        if(info is null)
        {
            return RedirectWithError("Error obteniendo informacion de Google");
        }
        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if (result.Succeeded)
        {
            return Redirect("/notes");
        }
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
        {
            return RedirectWithError("No se obtuvo el email del usuario");
        }
        var user = await _userManager.FindByEmailAsync(email) ?? new User
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
        };
        await _userManager.CreateAsync(user);
        await _userManager.AddLoginAsync(user, info);
        await _signInManager.SignInAsync(user, isPersistent: false);
        return Redirect("/notes");
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/notes");
    }

    private IActionResult RedirectWithError(string message)
    {
        var encoded = Uri.EscapeDataString(message);
        return Redirect($"/register?error={encoded}");
    }
}
