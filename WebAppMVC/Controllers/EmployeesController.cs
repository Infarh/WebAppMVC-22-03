using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using WebAppMVC.Services.Interfaces;
using WebAppMVC.ViewModels;

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

    public IActionResult Details(int id)
    {
        var employee = _EmployeesStore.GetById(id);
        if (employee is null)
            return NotFound();

        return View(new EmployeesViewModel
        {
            Id = employee.Id,
            LastName = employee.LastName,
            FirstName = employee.FirstName,
            Patronymic = employee.Patronymic,
            Birthday = employee.Birthday,
        });
    }

    public IActionResult Create() => View();

    public IActionResult Edit(int id)
    {
        var employee = _EmployeesStore.GetById(id);
        if (employee is null)
            return NotFound();

        return View(new EmployeesViewModel
        {
            Id = employee.Id,
            LastName = employee.LastName,
            FirstName = employee.FirstName,
            Patronymic = employee.Patronymic,
            Birthday = employee.Birthday,
        });
    }

    [HttpPost]
    public IActionResult Edit(EmployeesViewModel Model)
    {
        var employee = new Employee
        {
            Id = Model.Id,
            LastName = Model.LastName,
            FirstName = Model.FirstName,
            Patronymic = Model.Patronymic,
            Birthday = Model.Birthday,
        };

        var success = _EmployeesStore.Edit(employee);

        if (!success)
            return NotFound();

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id) => View();
}
