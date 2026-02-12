using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class JobSaved
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign key to JobSeekerProfile
        public Guid JobSeekerProfileId { get; set; }
        public virtual JobSeekerProfile JobSeekerProfile { get; set; } = null!;

        // Foreign key to JobPost
        public Guid JobPostId { get; set; }
        public virtual JobPost JobPost { get; set; } = null!;
    }

}
