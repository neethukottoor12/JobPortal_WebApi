using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.DTOs
{
    public class ScheduledInterviewDto
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; }
        public string JobseekerUsername { get; set; }

        public Guid ApplicationId { get; set; }

        public DateTime? Date { get; set; }


        public JobInterviewStatus Status { get; set; }
        public string CompanyUserName { get; set; }

    }
}
