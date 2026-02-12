using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.DTOs
{
    public class JobPostResponseDtos
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; }
        public string JobSummary { get; set; }
        public Guid LocationId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid IndustryId { get; set; }
        public DateTime PostedDate { get; set; }

       

    }
}
