using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.DTOs
{
    public class JobApplicationsDto
    {

        public Guid ApplicationId { get; set; }
        public Guid JobPostId { get; set; }
        public string JobTitle { get; set; }

        public Guid ApplicantId { get; set; }
        public Guid JobSeekerProfileId { get; set; }
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? ProfileImageBase64 { get; set; }
        public List<string> Qualifications { get; set; }
        public int TotalYearsOfExperience { get; set; }

        public DateTime AppliedDate { get; set; }

    }
}
