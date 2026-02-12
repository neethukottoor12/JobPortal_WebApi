//using Domain.Models;
using Domain.Service.Job.DTOs;
using System.ComponentModel.Design;

namespace Domain.Service.Job.Interfaces
{
    public interface IJobService
    {

        public Task<bool> VerifyJobSeekerId(Guid JobSeekerId);
        public Task<List<JobPostDto>> GetAllJobsList();
        public Task<bool> insertIntoJobApplication(Guid JobPostId, Guid ProfileId);
        public Task<List<JobSearchDto>> searchJobbyCompanyId(Guid companyID);
        public Task<List<JobSearchDto>> searchJobbyLocationId(Guid locationID);
        public Task<bool> saveJobforLater(Guid JobPostId, Guid seekerprofileID);
        public Task<bool> deleteJobforLater(Guid JobPostId);
        public Task<List<JobPostDto>> getAllAppliedJobs(Guid jobSeekerId);

    }
}