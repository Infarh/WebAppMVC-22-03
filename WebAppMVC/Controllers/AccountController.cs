using Identity.DAL.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebAppMVC.ViewModels.Identity;

namespace WebAppMVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _UserManager;
    private readonly SignInManager<User> _SignInManager;
    private readonly ILogger<AccountController> _Logger;

    public AccountController(
        UserManager<User> UserManager,
        SignInManager<User> SignInManager,
        ILogger<AccountController> Logger)
    {
        _UserManager = UserManager;
        _SignInManager = SignInManager;
        _Logger = Logger;
    }

    #region Регистрация

    public IActionResult Register() => View(new RegisterUserViewModel());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserViewModel Model)
    {
        if (!ModelState.IsValid)
            return View(Model);

        var user = new User
        {
            UserName = Model.UserName,
        };

        var result = await _UserManager.CreateAsync(user, Model.Password);
        if (result.Succeeded)
        {
            _Logger.LogInformation("Пользователь {0} успешно создан", user);

            await _SignInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);
        return View(Model);
    }

    #endregion

    #region Вход в систему

    public IActionResult Login(string? ReturnUrl = null) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel Model)
    {
        if (!ModelState.IsValid)
            return View(Model);

        var result = await _SignInManager.PasswordSignInAsync(
            Model.UserName,
            Model.Password,
            Model.RememberMe,
            true);

        if (result.Succeeded)
        {
            _Logger.LogInformation("Пользователь {UserName} успешно вошёл в систему", Model.UserName);

            //return Redirect(Model.ReturnUrl); - опасно!

            //if (Url.IsLocalUrl(Model.ReturnUrl))
            //    return Redirect(Model.ReturnUrl);
            //return RedirectToAction("Index", "Home");

            return LocalRedirect(Model.ReturnUrl ?? "/");
        }

        ModelState.AddModelError("", "Неверное имя пользователя или пароль");
        _Logger.LogWarning("Ошибка при входе в систему {0}", Model.UserName);

        return View(Model);
    }

    #endregion

    public async Task<IActionResult> Logout()
    {
        await _SignInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied() => View();
}
