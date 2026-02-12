using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ProfileQualification
    {
        public Guid JobSeekerProfileId { get; set; }
        public JobSeekerProfile JobSeekerProfile { get; set; }
        public Guid QualificationId { get; set; }
        public Qualification Qualification { get; set; }
    }
}
