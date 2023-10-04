﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Application.Common;

public class AuthRequest
{
    [Required(ErrorMessage = "Informe o usuário")]
    [BindProperty(Name = "email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe a senha")]
    [BindProperty(Name = "password")]
    public string Password { get; set; }
}
