using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Profile.DTOs
{
    public class ProfileDto
    {
       

        public Guid JobSeekerId { get; set; }
        public string? ProfileName { get; set; }
        public Guid LocationId { get; set; }
        public string? ProfileSummary { get; set; }
    }
}
