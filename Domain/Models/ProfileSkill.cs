using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ProfileSkill
    {
        public Guid JobSeekerProfileId { get; set; }
        public JobSeekerProfile JobSeekerProfile { get; set; }

        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
