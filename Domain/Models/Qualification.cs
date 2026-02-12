using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Qualification
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    

    public ICollection<JobPostQualification> JobPostQualifications { get; set; }
    public ICollection<ProfileQualification> ProfileQualification { get; set; } = null!;

}
