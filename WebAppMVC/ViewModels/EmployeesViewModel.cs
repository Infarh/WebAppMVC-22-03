using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.ViewModels;

public class EmployeesViewModel : IValidatableObject
{
    [HiddenInput(DisplayValue = false)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Фамилия является обязательной")]
    [Display(Name = "Фамилия")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Максимальная длина строки 100 символов, минимальная - 2")]
    [RegularExpression(@"([А-ЯЁ][а-яё\-0-9]+)|([A-Z][a-z]+)", ErrorMessage = "Строка имела неверный формат")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Имя")]
    [StringLength(100, ErrorMessage = "Максимальная длина строки 100 символов")]
    [RegularExpression(@"([А-ЯЁ][а-яё\-0-9]+)|([A-Z][a-z]+)", ErrorMessage = "Строка имела неверный формат")]
    public string? FirstName { get; set; }

    [Display(Name = "Отчество")]
    [StringLength(100, ErrorMessage = "Максимальная длина строки 100 символов")]
    [RegularExpression(@"([А-ЯЁ][а-яё\-0-9]+)|([A-Z][a-z]+)", ErrorMessage = "Строка имела неверный формат")]
    public string? Patronymic { get; set; }

    [Display(Name = "Дата рождения")]
    [Range(18, 85, ErrorMessage = "Возраст должен быть в интервале от 18 до 85 лет")]
    public int Age { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext Context)
    {
        if(LastName == "Иванов" && Age > 40)
            yield return new ValidationResult("Иванов не должен быть старше 40 лет", new[] { nameof(LastName), nameof(Age) });

        yield return ValidationResult.Success!;
    }
}
