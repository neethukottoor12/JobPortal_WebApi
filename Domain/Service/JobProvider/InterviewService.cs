using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.JobProvider.DTOs;
using Domain.Service.JobProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider
{
    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IMapper mapper;
        public InterviewService(IInterviewRepository interviewRepository, IMapper mapper)
        {
            _interviewRepository = interviewRepository;
            this.mapper = mapper;
        }

        public async Task<Interview> scheduleinterview(InterviewsheduleDtos interview, CompanyUser userId)
        {
            return await _interviewRepository.scheduleInterview(interview, userId);
        }
        public async Task<PagedList<Interview>> scheduledInterviewList(Guid companyid, InterviewParams param)
        {
            return await _interviewRepository.scheduledInterviewList(companyid, param);
        }
        public async Task<bool> removeInterview(Guid id)
        {
            return await _interviewRepository.removeInterview(id);
        }
    }
}
