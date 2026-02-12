using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public partial class JobPost
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string JobTitle { get; set; } = null!;
    public string JobSummary { get; set; } = null!;

    public Guid LocationId { get; set; }
    public virtual Location Location { get; set; } = null!;

    public Guid CompanyId { get; set; }
    public virtual JobProviderCompany Company { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public virtual JobCategory Category { get; set; } = null!;

    public Guid IndustryId { get; set; }
    public virtual Industry Industry { get; set; } = null!;

    public Guid PostedBy { get; set; }
    public virtual CompanyUser PostedByNavigation { get; set; } = null!;

    public DateTime PostedDate { get; set; }

    public virtual ICollection<JobResponsibility> JobResponsibilities { get; set; } = new List<JobResponsibility>();
   
    public ICollection<JobPostSkill> JobPostSkills { get; set;} = new List<JobPostSkill>();
    public ICollection<JobPostQualification> JobPostQualifications { get; set;} = new List<JobPostQualification>();
    public virtual ICollection<JobApplication> JobApplications { get; set; }
    = new List<JobApplication>();


}
