using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.Authuser.Interfaces;
using Domain.Service.JobProvider.DTOs;
using Domain.Service.JobProvider.Interfaces;
using JobPortal_Project.API.JobProvider.RequestObjects;
using JobPortal_Project.Controllers;
using JobPortal_Project.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal_Project.API.JobProvider
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "COMPANY_USER")]
    public class InterviewController : BaseApiController<InterviewController>
    {
        private readonly IInterviewService _interviewService;
        private readonly IMapper mapper;
        private readonly IAuthUserService _authUserService;
        public InterviewController(IInterviewService interviewService, IMapper mapper, IAuthUserService authUserService)
        {
            _interviewService = interviewService;
            this.mapper = mapper;
            _authUserService = authUserService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("company/company-user/{companyuserid}/ScheduleInterview")]
        public async Task<ActionResult> ScheduleInterview(InterviewSheduleObject interviewSheduleObject, Guid companyuserid)
        {
            try
            {
                var user = await _authUserService.GetUser(companyuserid);
                if (user == null)
                {
                    return NotFound("User Not Found");
                }
                var interviewdto = mapper.Map<InterviewsheduleDtos>(interviewSheduleObject);
                Interview interview = await _interviewService.scheduleinterview(interviewdto, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [AllowAnonymous]
        [HttpGet]
        [Route("company/company-user/{companyid}/FetchInterviewlist")]
        public async Task<ActionResult> ScheduledInterviewList(Guid companyid, [FromQuery] InterviewParams param)
        {
            try
            {
                PagedList<Interview> scheduledInterview = await _interviewService.scheduledInterviewList(companyid, param);
                Response.AddPaginationHeader(scheduledInterview.CurrentPage, scheduledInterview.PageSize, scheduledInterview.TotalCount, scheduledInterview.TotalPages);
                var scheduledinterviewdto = mapper.Map<PagedList<ScheduledInterviewDto>>(scheduledInterview);
                if (scheduledinterviewdto == null)
                    return NotFound("No scheduled Interviews");
                else
                    return Ok(scheduledinterviewdto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [AllowAnonymous]
        [HttpDelete]
        [Route("company/company-user/{intererviewid}/cancelInterview")]
        public async Task<ActionResult> cancelInterview(Guid intererviewid)
        {
            try
            {
                var result = await _interviewService.removeInterview(intererviewid);
                if (result == true)
                {
                    return Ok("Successfully cancel the interview");
                }
                else
                {
                    return NotFound("Not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
