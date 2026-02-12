using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public partial class Interview
    {

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Job))]
        public Guid? JobId { get; set; }

        public virtual JobPost? Job { get; set; }
        [ForeignKey(nameof(Jobseeker))]
        public Guid? interviewee { get; set; }
        public virtual JobSeeker? Jobseeker { get; set; }
        [ForeignKey(nameof(Application))]
        public Guid? ApplicationId { get; set; }
        public virtual JobApplication? Application { get; set; }

        public DateTime? Date { get; set; }


        public JobInterviewStatus Status { get; set; }
        [ForeignKey(nameof(CompanyUser))]
        public Guid? SheduledBy { get; set; }

        public virtual CompanyUser? CompanyUser { get; set; }





        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public virtual JobProviderCompany Company { get; set; }

    }
}
