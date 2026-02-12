using Domain.Models;
using Domain.Service.Job.DTOs;
using Domain.Service.Job.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.Service.Job
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<bool> VerifyJobSeekerId(Guid SeekerId)
        {
            return await _jobRepository.VerifyJobSeekerId(SeekerId);
        }
        public async Task<List<JobPostDto>> GetAllJobsList()
        {
            return await _jobRepository.GetAllJobsListAsync();
        }

        //public async Task<bool> VerifyJobPost(Guid JobPostId)
        //{
        //    return await _jobRepository.VerifyJobPostIdAsync(JobPostId);
        //}

        //insert into JobApplication
        public async Task<bool> insertIntoJobApplication(Guid JobPostId, Guid ProfileId)
        {
            return await _jobRepository.insertIntoJobApplication(JobPostId, ProfileId);

        }

        //search by companyid
        public async Task<List<JobSearchDto>> searchJobbyCompanyId(Guid companyID)
        {
            return await _jobRepository.searchJobbyCompanyId(companyID);
        }
        //search by locationid
        public async Task<List<JobSearchDto>> searchJobbyLocationId(Guid locationID)
        {
            return await _jobRepository.searchJobbyLocationIDAsync(locationID);
        }
        //Save job
        public async Task<bool> saveJobforLater(Guid JobPostId, Guid seekerprofileID)
        {
            return await _jobRepository.saveJobforLaterAsync(JobPostId, seekerprofileID);
        }
        //delete jobsaved
        public async Task<bool> deleteJobforLater(Guid jobPostId)
        {
            return await _jobRepository.deleteJobforLater(jobPostId);
        }
        public async Task<List<JobPostDto>> getAllAppliedJobs(Guid jobSeekerId)
        {
            return await _jobRepository.getAllAppliedJobsAsync(jobSeekerId);
        }

    }
}