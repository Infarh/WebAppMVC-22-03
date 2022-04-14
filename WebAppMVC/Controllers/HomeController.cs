using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Services.Interfaces;

namespace WebAppMVC.Controllers;

//[Controller]
public class HomeController : Controller
{
    private readonly IEmployeesStore _EmployeesStore;
    private readonly ILogger<HomeController> _Logger;

    public HomeController(IEmployeesStore EmployeesStore, ILogger<HomeController> Logger)
    {
        _EmployeesStore = EmployeesStore;
        _Logger = Logger;
    }

    public IActionResult Index()
    {
        var employees = _EmployeesStore.GetAll();

        //ViewBag.Title = "123";
        //var title = ViewData["Title"];
        return View(employees);
    }
}
