using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.DTOs
{
   public class JobProviderDto_cs
    {
        public Guid Id { get; set; }

        public string JobTitle { get; set; } = null!;

        public string JobSummary { get; set; } = null!;

        public Guid JobLocation { get; set; }

        public Guid Company { get; set; }

        public Guid Category { get; set; }

        public Guid Industry { get; set; }

        public Guid PostedBy { get; set; }

        public DateTime PostedDate { get; set; }
    }
}
