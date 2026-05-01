using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Job.DTOs
{
    public class UpdateJobPostDtos
    {
        public Guid Id { get; set; }

        public string JobTitle { get; set; }
        public string JobSummary { get; set; }

        public string LocationName { get; set; }
        public Guid LocationId { get; set; }
        public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public string IndustryName { get; set; }
        public Guid IndustryId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }

        public DateTime PostedDate { get; set; }

        public List<ResponsibilityDto> Responsibilities { get; set; }
        public List<SkillListDtos> Skills { get; set; }
        public List<QualificationListDtos> Qualifications { get; set; }
    }
}
