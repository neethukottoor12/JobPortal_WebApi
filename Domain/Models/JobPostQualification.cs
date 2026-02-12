using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class JobPostQualification
    {
        public Guid JobPostId { get; set; }
        public JobPost JobPost { get; set; }

        public Guid QualificationId { get; set; }
        public Qualification Qualification { get; set; }

    }
}
