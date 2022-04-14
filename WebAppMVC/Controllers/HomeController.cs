using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Services.Interfaces;

namespace WebAppMVC.Controllers;

//[Controller]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _Logger;

    public HomeController(IEmployeesStore EmployeesStore, ILogger<HomeController> Logger)
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
