using Domain.Helpers;
using Domain.Models;
using Domain.Service.JobProvider.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.Interfaces
{
    public interface IInterviewRepository
    {
        Task<Interview> scheduleInterview(InterviewsheduleDtos interview, CompanyUser user);
        Task<PagedList<Interview>> scheduledInterviewList(Guid companyid, InterviewParams param);
        Task<bool> removeInterview(Guid id);
    }
}
