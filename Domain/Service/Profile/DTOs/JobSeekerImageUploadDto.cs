using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Profile.DTOs
{
    public class JobSeekerImageUploadDto
    {
        // Foreign key to JobSeekerProfile
        public Guid JobSeekerProfileId { get; set; }
        public string FileName { get; set; } = null!;

        // MIME type (e.g., "image/jpeg", "image/png")
        public string ContentType { get; set; } = null!;
        // File to upload
        public IFormFile File { get; set; } = null!;
    }

}