using System.ComponentModel.DataAnnotations;

namespace JobPortal_Project.API.JobProvider.RequestObjects
{
    public class JobProviderLoginRequest
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
