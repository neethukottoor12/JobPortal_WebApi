using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Profile.DTOs
{
    public class ResumeUploadDto
    {
        public Guid JobSeekerProfileId { get; set; }
        public string? Title { get; set; }

        public IFormFile ResumeFile { get; set; }
    }

}