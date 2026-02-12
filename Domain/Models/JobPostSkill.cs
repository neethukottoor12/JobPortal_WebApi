using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public partial class JobPostSkill
    {
        public Guid JobPostId { get; set; }
        public JobPost JobPost { get; set; }
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
