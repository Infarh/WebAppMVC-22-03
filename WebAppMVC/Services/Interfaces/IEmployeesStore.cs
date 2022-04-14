using WebAppMVC.Models;

namespace WebAppMVC.Services.Interfaces;

public interface IEmployeesStore
{
    IEnumerable<Employee> GetAll();

    Employee? GetById(int Id);

    int Add(Employee item);

    bool Edit(Employee item);

    bool Remove(int Id);
}
