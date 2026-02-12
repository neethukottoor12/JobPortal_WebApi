using System.ComponentModel.DataAnnotations;

namespace JobPortal_Project.API.JobProvider.RequestObjects
{
    public class CompanyUserRequest
    {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; } = null!;
        [RegularExpression(@"^\+?[1-9]\d{7,14}$",
       ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; } = null!;

        public string Password { get; set; }
    }
}
