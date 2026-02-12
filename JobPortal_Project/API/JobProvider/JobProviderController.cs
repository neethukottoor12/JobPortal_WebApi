using AutoMapper;
using Domain.Models;
using Domain.Service.Job.DTOs;
using Domain.Service.JobProvider;
using Domain.Service.JobProvider.DTOs;
using Domain.Service.JobProvider.Interfaces;
using Domain.Service.Login.DTOs;
using Domain.Service.Login.Interfaces;
using Domain.Service.SignUp.DTOs;
using JobPortal_Project.API.JobProvider.RequestObjects;
using JobPortal_Project.API.JobSeeker.RequestObjects;
using JobPortal_Project.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal_Project.API.JobProvider
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "JOB_PROVIDER")]
    public class JobProviderController : BaseApiController<JobProviderController>
    {
        private readonly IJobProviderService _jobProviderService;
        private readonly IMapper mapper;
        private readonly ILoginRequestService loginRequestService;
        public JobProviderController(IJobProviderService jobProviderService, IMapper mapper, ILoginRequestService loginRequestService)
        {
            _jobProviderService = jobProviderService;
            this.mapper = mapper;
            this.loginRequestService = loginRequestService;
        }

        [HttpPost]
        [Route("Job-Provider/Signup")]
        [AllowAnonymous]
        
        [ProducesResponseType(typeof(JobProviderLoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> createJobProviderSignupRequest([FromBody]JobProviderSignupRequest data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            try
            {
                var jobprovidersignuprequestdto = mapper.Map<JobProviderSignupRequestDto>(data);
                await _jobProviderService.CreateSignupRequest(jobprovidersignuprequestdto);
                return Ok(data);
            }
            catch (InvalidOperationException ex)
            {
                // This catches "Email already exists"
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                // Optional: fallback for unexpected errors
                return StatusCode(500, "Something went wrong");
            }

        }
        [HttpGet]
        [Route("job-provider/signup/{signupRequestId}/verify-email")]
        [AllowAnonymous]
        public async Task<ActionResult> VerifyJobProviderEmail(Guid signupRequestId)
        {
            var isVerified = await _jobProviderService.VerifyEmailAsync(signupRequestId);
            if (isVerified)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("job-provider/signup/{jobProviderSignupRequestId}/set-password")]
        [AllowAnonymous]
        public async Task<ActionResult> createJobProviderSignupRequest(Guid jobProviderSignupRequestId, [FromBody] string password)
        {
            await _jobProviderService.CreateJobProvider(jobProviderSignupRequestId, password);
            return Ok("Password set successfully");
        }
        [HttpPost]
        [Route("jobprovider/login")]
        [AllowAnonymous]

        public async Task<ActionResult<JobProviderLoginDto>> Login(JobProviderLoginRequest logdata)
        {
            
            var user = await loginRequestService.loginJP(logdata.Email, logdata.Password);
            if (user == null)
            {
                return BadRequest("Login Failed");
            }
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("company/{companyId}/job-provider/{jobproviderId}/Postjob")]

        public async Task<IActionResult> PostJob(JobPostRequest request)
        {
            try
            {
                var job = mapper.Map<JobPost>(request);
                Guid id = await _jobProviderService.PostJob(job, request.Responsibilities, request.SkillIds, request.QualificationIds);
                return Ok("The job id for the posted job is" + id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("company/{companyId}/GetAllJobs")]
        public async Task<IActionResult> GetAllJobs(Guid companyId)
        {
            try
            {
                List<JobPost> jobs = await _jobProviderService.GetJobs(companyId);
                return Ok(mapper.Map<List<JobPostsDtos>>(jobs));

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [AllowAnonymous]
        [HttpGet]
        [Route("company/{companyId}/job-provider/{jobproviderId}/GetAllJobsByProvider")]
        public async Task<IActionResult> GetAllJobsByProvider(Guid companyId, Guid jobproviderId)
        {
            try
            {
                List<JobPost> jobs = await _jobProviderService.GetAllJobsByProvider(companyId, jobproviderId);
                return Ok(mapper.Map<List<JobPostsDtos>>(jobs));

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [AllowAnonymous]
        [HttpPut]
        [Route("company/{companyId}/job-provider/{jobproviderId}/job/{id}/UpdateJob")]
        public async Task<IActionResult> UpdateJob(JobPostRequest request, Guid id)
        {
            try
            {
                var job = mapper.Map<JobPost>(request);
                job.JobResponsibilities = request.Responsibilities
                .Select(r => new JobResponsibility
                     {
                       Id = Guid.NewGuid(),
                       Description=r,
                       JobPost = job.Id
                      })
                  .ToList();

                job.JobPostSkills = request.SkillIds
                .Select(skillId => new JobPostSkill
                 {
                    JobPostId = job.Id,
                     SkillId = skillId
                 })
                   
                .ToList();

                job.JobPostQualifications=request.QualificationIds
                    .Select(qual_id=>new JobPostQualification
                    {
                        JobPostId = job.Id,
                        QualificationId = qual_id
                    })
                    .ToList();


                var jobtoupdate = await _jobProviderService.Update(job, id);
                var response = mapper.Map<JobPostResponseDtos>(jobtoupdate);


                return Ok(response); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpDelete]
        [Route("company/{companyId}/job-provider/{jobproviderId}/job/{id}/DeleteJob")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            try
            {
                var result = await _jobProviderService.DeleteJob(id);
                if(result)
                {
                    return Ok("Job Deleted Successfully");
                }
                else
                {
                    return NotFound("Company not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("job-provider/{jobproviderId}/GetJobApplicants")]
        public async Task<IActionResult> GetAllJobApplicants(Guid jobproviderId)
        {
            try
            {
                List<JobApplication> applications = await _jobProviderService.GetAllJobApplicants(jobproviderId);
                return Ok(mapper.Map<List<JobApplicationsDto>>(applications));


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       

    }
}
