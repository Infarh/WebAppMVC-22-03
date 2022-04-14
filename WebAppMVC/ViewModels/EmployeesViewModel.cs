using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.ViewModels;

public class EmployeesViewModel
{
    [HiddenInput(DisplayValue = false)]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Фамилия")]
    public string LastName { get; set; }

    public string FirstName { get; set; }

    public string Patronymic { get; set; }


    public DateTime Birthday { get; set; }
}
