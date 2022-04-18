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
            Age = employee.Age,
        });
    }

    public IActionResult Create() => View("Edit", new EmployeesViewModel());

    public IActionResult Edit(int? id)
    {
        if (id is null)
        {
            return View(new EmployeesViewModel());
        }

        var employee = _EmployeesStore.GetById((int)id);
        if (employee is null)
            return NotFound();

        return View(new EmployeesViewModel
        {
            Id = employee.Id,
            LastName = employee.LastName,
            FirstName = employee.FirstName,
            Patronymic = employee.Patronymic,
            Age = employee.Age,
        });
    }

    [HttpPost]
    public IActionResult Edit(EmployeesViewModel Model)
    {
        if(Model.LastName == "Иванов" && Model.FirstName == "Иван" && Model.Patronymic == "Иванович")
            ModelState.AddModelError("", "Банальное сочетание ФИО");

        if (!ModelState.IsValid)
            return View(Model);
        
        var employee = new Employee
        {
            Id = Model.Id,
            LastName = Model.LastName,
            FirstName = Model.FirstName,
            Patronymic = Model.Patronymic,
            Age = Model.Age,
        };

        if (employee.Id == 0)
        {
            var id = _EmployeesStore.Add(employee);
            return RedirectToAction("Details", new { id });
        }

        var success = _EmployeesStore.Edit(employee);

        if (!success)
            return NotFound();

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
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
            Age = employee.Age,
        });
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!_EmployeesStore.Remove(id))
            return NotFound();

        return RedirectToAction("Index");
    }
}
