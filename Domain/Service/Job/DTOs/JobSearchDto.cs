using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Job.DTOs
{
    public class JobSearchDto
    {
        public string JobTitle { get; set; }
        public string JobSummary { get; set; }
        public Guid LocationId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
