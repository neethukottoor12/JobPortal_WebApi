using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Skill
{
    public Guid Id { get; set; }= Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

   public ICollection<JobPostSkill> jobPostSkills { get; set; }= new List<JobPostSkill>();
    public ICollection<ProfileSkill> ProfileSkill { get; set; } = null!;
}
