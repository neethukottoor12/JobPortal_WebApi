using System.ComponentModel.DataAnnotations;

namespace JobPortal_Project.API.JobProvider.RequestObjects
{
    public class AddCompanyRequest
    {
        public string LegalName { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public Guid IndustryId { get; set; }
        [EmailAddress]
        public string Email { get; set; } = null!;
        [RegularExpression(@"^\+?[1-9]\d{7,14}$",
       ErrorMessage = "Invalid phone number format.")]
        public long Phone { get; set; }

        public string Address { get; set; } = null!;

        public string Website { get; set; } = null!;

        public Guid Location { get; set; }

    }
}
