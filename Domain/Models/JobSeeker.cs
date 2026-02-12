using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public partial class JobSeeker
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? UserName { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    [RegularExpression(@"^\+?[1-9]\d{7,14}$",
    ErrorMessage = "Invalid phone number format.")]

    public string Phone { get; set; } = null!;
    [EmailAddress]
    public string? Email { get; set; } = null!;
    public Domain.Enums.Role Role { get; set; }

    // Foreign key to SystemUser
    public Guid SystemUserId { get; set; }
    [ForeignKey("SystemUserId")]
    public virtual SystemUser SystemUser { get; set; } = null!;
}