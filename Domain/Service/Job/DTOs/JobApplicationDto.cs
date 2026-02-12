using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Job.DTOs
{
    public class JobApplicationDto
    {
        public Guid JobPostId { get; set; }
        public Guid ProfileId { get; set; }

    }
}