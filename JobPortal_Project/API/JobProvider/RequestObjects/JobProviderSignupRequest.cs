using System.ComponentModel.DataAnnotations;

namespace JobPortal_Project.API.JobProvider.RequestObjects
{
    public class JobProviderSignupRequest
    {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
       
        [RegularExpression(@"^\+?[1-9]\d{7,14}$",
        ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; } = null!;
    }
}
