using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.DTOs
{
    public class ApplicantDetailsDto
    {
        // Application Info
        public Guid ApplicationId { get; set; }
        public DateTime AppliedDate { get; set; }

        // Job Info
        public Guid JobPostId { get; set; }
        public string JobTitle { get; set; }
        public string JobSummary { get; set; }
        public Guid CompanyId { get; set; }
        public Guid JobLocationId { get; set; }

        // Applicant Basic Info
        public Guid ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Profile Info
        public Guid JobSeekerProfileId { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfileSummary { get; set; }
        public string? LocationName { get; set; }

        // Profile Image
        public string? ProfileImageBase64 { get; set; }

        // Resume
        public string? ResumeFileName { get; set; }
        public string? ResumeBase64 { get; set; }

        // Qualifications
        public List<string> Qualifications { get; set; } = new();

        // Skills
        public List<string> Skills { get; set; } = new();

        // Experience
        public double TotalYearsOfExperience { get; set; }
        public List<ExperienceDto> WorkExperiences { get; set; } = new();
    }

    

}
