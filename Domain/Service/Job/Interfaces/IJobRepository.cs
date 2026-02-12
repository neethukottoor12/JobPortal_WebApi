//using Domain.Models;
using Domain.Service.Job.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Job.Interfaces
{
    public interface IJobRepository
    {
        public Task<List<JobPostDto>> GetAllJobsListAsync();
        public Task<bool> VerifyJobSeekerId(Guid JobSeekerId);
       
        public Task<bool> insertIntoJobApplication(Guid JobPostId, Guid ProfileId);
        public Task<List<JobSearchDto>> searchJobbyCompanyId(Guid companyID);
        public Task<List<JobSearchDto>> searchJobbyLocationIDAsync(Guid locationID);
        public Task<bool> saveJobforLaterAsync(Guid JobPostId, Guid seekerprofileID);
        public Task<bool> deleteJobforLater(Guid JobPostId);
        public Task<List<JobPostDto>> getAllAppliedJobsAsync(Guid jobSeekerId);


    }
}