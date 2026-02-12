using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public partial class SignUpRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? UserName { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }
    [RegularExpression(@"^\+?[1-9]\d{7,14}$",
    ErrorMessage = "Invalid phone number format.")]


    public string Phone { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;

    public Status Status { get; set; }
}
