using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.DTOs
{
    public class JobApplicationsDto
    {
       
        public Guid Id { get; set; }
        /*        [ForeignKey(nameof(JobPost))]*/
        public Guid JobPostId { get; set; }
        //[ForeignKey(nameof(Seeker))]
        public Guid Applicant { get; set; }
        /*
                [ForeignKey(nameof(Resume))]*/
        public Guid Resume_id { get; set; }

        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

       
    }
}
