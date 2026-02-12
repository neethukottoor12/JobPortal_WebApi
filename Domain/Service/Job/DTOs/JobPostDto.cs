//using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Job.DTOs
{
    public class JobPostDto
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; } = null!;
        public string JobSummary { get; set; } = null!;
        public Guid LocationId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
