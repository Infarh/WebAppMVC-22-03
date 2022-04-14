using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers;

//[Controller]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _Logger;

    public HomeController(ILogger<HomeController> Logger)
    {
        _Logger = Logger;
    }

    public IActionResult Index()
    {
        //ViewBag.Title = "123";
        //var title = ViewData["Title"];
        return View();
    }
}
