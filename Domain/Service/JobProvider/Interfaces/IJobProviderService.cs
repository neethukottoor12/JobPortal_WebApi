using Domain.Models;
using Domain.Service.SignUp.DTOs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;


namespace Domain.Service.JobProvider.Interfaces
{
    public interface IJobProviderService
    {
        Task CreateSignupRequest(JobProviderSignupRequestDto data);
        Task<bool> VerifyEmailAsync(Guid jobProviderSignupRequestId);
        Task CreateJobProvider(Guid jobProviderSignupRequestId, string password);
        public Task<Guid> PostJob(JobPost job,List<string> Responsibilities,List<Guid> Skillids, List<Guid> QualificationIds);
        public Task<List<JobPost>> GetJobs(Guid companyId);
        public Task<List<JobPost>> GetAllJobsByProvider(Guid companyId, Guid jobproviderId);
        public Task<JobPost> Update(JobPost job, Guid id);
        public Task<bool> DeleteJob(Guid id);
        public Task<List<JobProviderCompany>> GetCompany(Guid jobproviderId);
        public Task<List<JobApplication>> GetAllJobApplicants(Guid jobproviderId);


    }
}
