using AutoMapper;
using Domain.Service.Login.DTOs;
using Domain.Service.Login.Interfaces;
using Domain.Service.SignUp.DTOs;
using Domain.Service.SignUp.Interfaces;
using JobPortal_Project.API.JobSeeker.RequestObjects;
using JobPortal_Project.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal_Project.API.JobSeeker
{
    [ApiController]

    [Route("API/JobSeeker")]
    public class JobSeekerController : BaseApiController<JobSeekerController>
    {
        public ISignUpRequestService jobSeekerService { get; set; }
        public ILoginRequestService loginRequestService { get; set; }
        public IMapper mapper { get; set; }
        public JobSeekerController(ISignUpRequestService _jobSeekerService, ILoginRequestService _loginRequestService, IMapper _mapper)
        {
            jobSeekerService = _jobSeekerService;
            loginRequestService = _loginRequestService;
            mapper = _mapper;
        }
        [HttpPost]
        [ProducesResponseType(typeof(JobProviderLoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> createJobSeekerSignupRequest([FromBody]JobSeekerSignupRequest data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var JobSeekerSignupRequestDto = mapper.Map<JobSeekerSignupRequestDto>(data);
                await jobSeekerService.CreateSignupRequest(JobSeekerSignupRequestDto);
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
        [Route("job-seeker/signup/{jobSeekerSignupRequestId}/verify-email")]

        public async Task<ActionResult> VerifyJobSeekerEmail(Guid jobSeekerSignupRequestId)
        {
            try
            {
                var isverified = await jobSeekerService.VerifyEmailAsync(jobSeekerSignupRequestId);
                if (isverified)
                {
                    return Ok();
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpPost]
        [Route("job-seeker/signup/{jobSeekerSignupRequestId}/set-password")]
        public async Task<IActionResult> createJobSeekerSignupRequest(Guid jobSeekerSignupRequestId, [FromBody] string password)
        {
            try
            {
                await jobSeekerService.CreateJobseeker(jobSeekerSignupRequestId, password);
                return Ok("Password set successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("job-seeker/login")]
        public async Task<IActionResult> Login(JobSeekerLoginRequest logdata)
        {
            try
            {
                var user = await loginRequestService.login(logdata.Email, logdata.Password);
                if (user == null)
                    return BadRequest("Login Failed");
                else
                    return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}