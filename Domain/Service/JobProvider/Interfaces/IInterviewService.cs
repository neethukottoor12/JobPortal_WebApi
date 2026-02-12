using Domain.Helpers;
using Domain.Models;
using Domain.Service.JobProvider.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.Interfaces
{
    public interface IInterviewService
    {
        Task<Interview> scheduleinterview(InterviewsheduleDtos interview, CompanyUser userId);
        Task<PagedList<Interview>> scheduledInterviewList(Guid companyid, InterviewParams param);
        Task<bool> removeInterview(Guid id);
    }
}
