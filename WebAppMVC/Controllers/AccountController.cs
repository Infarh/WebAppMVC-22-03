using Identity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _User;
    private readonly SignInManager<User> _SignInManager;
    private readonly ILogger<AccountController> _Logger;

    public AccountController(
        UserManager<User> User, 
        SignInManager<User> SignInManager,
        ILogger<AccountController> Logger)
    {
        _User = User;
        _SignInManager = SignInManager;
        _Logger = Logger;
    }

    public IActionResult Register() => View();
    
    public IActionResult Login() => View();
    
    public IActionResult Logout() => View();

    public IActionResult AccessDenied() => View();
}
