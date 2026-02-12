using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class CompanyUser
{
   

    public Guid Id { get; set; }= Guid.NewGuid();

    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    [Required]
    public Enums.Role Role { get; set; }
    public string? UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; } = null!;
    [RegularExpression(@"^\+?[1-9]\d{7,14}$",
    ErrorMessage = "Invalid phone number format.")]
   
    public string Phone { get; set; } = null!;
    //[Required]
    [ForeignKey(nameof(CompanyNavigation))]

    public Guid? Company { get; set; }
    [JsonIgnore]
    public virtual JobProviderCompany? CompanyNavigation { get; set; }

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
}
