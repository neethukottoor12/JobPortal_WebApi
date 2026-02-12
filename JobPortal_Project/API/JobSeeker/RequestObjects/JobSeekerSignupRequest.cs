using System.ComponentModel.DataAnnotations;

namespace JobPortal_Project.API.JobSeeker.RequestObjects
{
    public class JobSeekerSignupRequest
    {
        public string? UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"^\+?[1-9]\d{7,14}$",
        ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
