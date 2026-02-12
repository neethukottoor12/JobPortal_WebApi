using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public partial class JobApplication
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign key to JobSeeker
        public Guid JobSeekerProfileId { get; set; }
        public virtual JobSeekerProfile JobSeekerProfile { get; set; } = null!;


        // Foreign key to JobPost
        public Guid JobPostId { get; set; }
        public virtual JobPost JobPost { get; set; } = null!;
        //resume
        [ForeignKey(nameof(Resume))]
        public Guid Resume_id { get; set; }
        public virtual Resume Resume { get; set; }
        //Jobseeker
        [ForeignKey(nameof(Seeker))]
        public Guid Applicant { get; set; }
        public virtual JobSeeker Seeker { get; set; }
        // Denormalized fields (copied from JobPost at application time)
        public string JobTitle { get; set; } = null!;
        public string JobSummary { get; set; } = null!;
        public Guid LocationId { get; set; }
        public Guid CompanyId { get; set; }

        // Extra metadata
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

    }
}
