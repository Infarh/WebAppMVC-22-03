using Microsoft.AspNetCore.Mvc;

using WebAppMVC.Services.Interfaces;

namespace WebAppMVC.Controllers;

public class EmployeesController : Controller
{
    private readonly IEmployeesStore _EmployeesStore;
    private readonly ILogger<EmployeesController> _Logger;

    public EmployeesController(IEmployeesStore EmployeesStore, ILogger<EmployeesController> Logger)
    {
        _EmployeesStore = EmployeesStore;
        _Logger = Logger;
    }

    public IActionResult Index()
    {
        var employees = _EmployeesStore.GetAll();

        return View(employees);
    }
}
