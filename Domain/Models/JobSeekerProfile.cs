using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public partial class JobSeekerProfile
{
    public Guid Id { get; set; }

    public Guid JobSeekerId { get; set; }


    //public Guid? ResumeId { get; set; }
    public virtual Resume Resume { get; set; } = null;
   

    public string? ProfileName { get; set; }

    public string? ProfileSummary { get; set; }
    public Guid? LocationId { get; set; }

    [ForeignKey("LocationId")]
    public virtual Location Location { get; set; } = null;
    public virtual JobSeekerImage JobSeekerImage { get; set; }



    public virtual ICollection<ProfileSkill> ProfileSkill { get; set; } = new List<ProfileSkill>();
    public virtual ICollection<ProfileQualification> ProfileQualification { get; set; } = new List<ProfileQualification>();
    public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
    public virtual ICollection<JobApplication> JobApplications { get; set; }
    = new List<JobApplication>();

}