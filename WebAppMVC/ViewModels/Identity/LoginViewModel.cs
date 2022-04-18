﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.ViewModels.Identity;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = null!;

    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }

    [HiddenInput(DisplayValue = false)]
    public string? ReturnUrl { get; set; }
}
