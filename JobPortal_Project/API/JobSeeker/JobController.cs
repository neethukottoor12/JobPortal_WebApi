using AutoMapper;
using Domain.Models;
using Domain.Service.Job;
using Domain.Service.Job.DTOs;
using Domain.Service.Job.Interfaces;
using Domain.Service.JobProvider.DTOs;
using JobPortal_Project.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace JobPortal_Project.API.JobSeeker
{
    [ApiController]
    [Route("API/JobSeeker/Job")]
    public class JobController : BaseApiController<JobController>
    {
        private readonly IJobService jobservice;
        private readonly IMapper mapper;

        public JobController(IJobService _jobService, IMapper _mapper)
        {
            jobservice = _jobService;
            mapper = _mapper;
        }
        //List all Jobs
        [HttpGet("{jobSeekerId}/Get-Jobs")]
        public async Task<IActionResult> GetAllJobs(Guid jobSeekerId)
        {
            try
            {
                var seekerExists = await jobservice.VerifyJobSeekerId(jobSeekerId);
                if (seekerExists == false)
                    return NotFound("Profile not found.");

                var jobs = await jobservice.GetAllJobsList();
                if (jobs == null)
                    return NotFound("No jobs found.");

                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpPost("/ApplyforJob")]
        
        public async Task<IActionResult> ApplyForJob([FromBody] JobApplicationDto dtoData)
        {
            try
            {
                var validateData = await jobservice.insertIntoJobApplication(dtoData.JobPostId, dtoData.ProfileId);
                if (validateData) return Ok("Applied for this Job successfully. ");
                return BadRequest("Not applied");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Search job by companyID
        [HttpGet("/Search/JobByCompanyId/{companyID}")]
        public async Task<IActionResult> SearchJobbycompanyID(Guid companyID)
        {
            try
            {
                var searchJob = await jobservice.searchJobbyCompanyId(companyID);
                if (searchJob != null) return Ok(searchJob);
                return BadRequest("Job is not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //SearchJob by LocationId
        [HttpGet("/Search/JobByLocationId/{locationID}")]
        public async Task<IActionResult> SearchJobbyLocationID(Guid locationID)
        {
            try
            {
                var searchJob = await jobservice.searchJobbyLocationId(locationID);
                if (searchJob != null) return Ok(searchJob);
                return BadRequest("Job is not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Save job
        [HttpPost("/SaveJob/{JobPostId}")]
        public async Task<IActionResult> SaveJob(Guid JobPostId, Guid seekerprofileID)
        {
            try
            {
                var saveJob = await jobservice.saveJobforLater(JobPostId, seekerprofileID);
                if (saveJob) return Ok("Job successfully saved");
                return BadRequest("Job not found or JobSeekerProfileID is not valid");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Delete SavedJob
        [HttpDelete("/DeleteSavedJob/{jobPostId}")] // Match the variable name below
        public async Task<IActionResult> DeleteSavedJob([FromRoute] Guid jobPostId)
        {
            try
            {
                bool issuccess = await jobservice.deleteJobforLater(jobPostId);
                if (issuccess) return Ok("Successfully deleted");
                return BadRequest("Not deleted");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Get Applications
        [HttpPost("/GetAllApplication")]
        public async Task<IActionResult> GetAllAppliedJobs(Guid jobSeekerId)
        {
            try
            {
                var validateData = await jobservice.VerifyJobSeekerId(jobSeekerId);
                if (validateData)
                {
                    var appliedJobs = await jobservice.getAllAppliedJobs(jobSeekerId);
                    return Ok(appliedJobs);
                }
                return BadRequest("No Applied Jobs");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }


}