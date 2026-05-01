using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.Interfaces
{
    public interface IJobProviderRepository
    {
        Task<Guid> AddSignupRequest(SignUpRequest signUpRequest);
        Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobProviderSignupRequestId);
        Task UpdateSignupRequest(SignUpRequest signUpRequest);
        public Task<Guid> Create(JobPost job);
        public Task<List<JobPost>> GetJobs(Guid companyId);
        public Task<List<JobPost>> GetAllJobsByProvider(Guid companyId, Guid jobproviderId);
        public Task<JobPost> UpdateAsync(JobPost Updatedjob, Guid id);
        public Task<bool> DeleteJob(Guid id);
        public Task<List<JobProviderCompany>> GetCompany(Guid jobproviderId);
        public Task<List<JobApplication>> GetAllJobApplicants(Guid jobproviderId);
        public Task<JobPost> GetJobsById(Guid id);
        public Task<JobApplication?> GetApplicantDetailsAsync(Guid applicationId);

    }
}
