namespace JobPortal_Project.API.JobProvider.RequestObjects
{
    public class JobPostRequest
    {
        public string JobTitle { get; set; } = null!;

        public string JobSummary { get; set; } = null!;
        /*        [ForeignKey(nameof(Location))]*/
        public Guid LocationId { get; set; }

        /*        [ForeignKey(nameof(Company))]*/
        public Guid CompanyId { get; set; }
        /*        [ForeignKey(nameof(JobCategory))]*/
        public Guid CategoryId { get; set; }
        /*        [ForeignKey(nameof(Industry))]*/
        public Guid IndustryId { get; set; }

        public Guid PostedBy { get; set; }

        public DateTime PostedDate { get; set; }

        public List<string> Responsibilities { get; set; } = new();
        public List<Guid> SkillIds { get; set; } = new();
        public List<Guid> QualificationIds { get; set; } = new();

    }
}
