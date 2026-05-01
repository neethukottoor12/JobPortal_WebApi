using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.JobProvider.DTOs;
using Domain.Service.JobProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly DbHireMeNowWebApiContext _context;
        private readonly IMapper _mapper;
        public InterviewRepository(IMapper mapper, DbHireMeNowWebApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<Interview> scheduleInterview(InterviewsheduleDtos interview, CompanyUser user)
        {
            try
            {
                var exist = await _context.Interviews.Where(a => a.ApplicationId == interview.ApplicationId).FirstOrDefaultAsync();
                if(exist!=null)
                {
                    throw new Exception("Interview already scheduled for this application.");
                }
                JobApplication application = await _context.JobApplications.Where(a => a.Id == interview.ApplicationId).Include(e => e.JobPost).FirstOrDefaultAsync();
                var seekerprofile = await _context.JobSeekerProfiles.Where(j => j.Id == application.JobSeekerProfileId).FirstOrDefaultAsync();
                var seeker = await _context.JobSeekers.Where(s => s.Id == seekerprofile.JobSeekerId).FirstOrDefaultAsync();
                var interviewtoschedule = _mapper.Map<Interview>(interview);
                interviewtoschedule.JobId = application.JobPostId;
                interviewtoschedule.ApplicationId = application.Id;
                interviewtoschedule.Status = Enums.JobInterviewStatus.SCHEDULED;
                interviewtoschedule.SheduledBy = user.Id;
                interviewtoschedule.interviewee = seeker.Id;
                interviewtoschedule.CompanyId = (Guid)user.Company;
                _context.Interviews.AddAsync(interviewtoschedule);
                await _context.SaveChangesAsync();
                return interviewtoschedule;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<PagedList<Interview>> scheduledInterviewList(Guid companyid, InterviewParams param)
        {
            var query = _context.Interviews
                .OrderByDescending(c => c.Date).Where(e => e.CompanyId == companyid).Include(e => e.Job).Include(e => e.Application).Include(e => e.Company)
                .Include(e => e.CompanyUser).Include(e => e.Jobseeker).AsQueryable();
            return await PagedList<Interview>.CreateAsync(query, param.PageNumber, param.PageSize);
        }
        public async Task<bool> removeInterview(Guid id)
        {
            var item = await _context.Interviews.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (item != null)
            {
                _context.Interviews.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
