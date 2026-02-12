using AutoMapper;
using Domain.Models;
using Domain.Service.Profile.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Profile.Intefaces
{
    public interface IProfileRepository
    {
        Task UpdateProfileAsync(JobSeekerProfile profile);
        Task<ProfileDto> ViewProfileDetailsAsync(Guid JobSeekerId);
        Task<bool> VerifyProfileIdAsync(Guid JobSeekerProfileId);
        Task<Guid> LoadResumeAsync(MemoryStream memoryStream, string title, Guid SeekerProfileId);
        Task<Guid> UpdateResumeAsync(MemoryStream memoryStream, string title, Guid SeekerProfileId);
        Task<Guid> LoadImageAsync(MemoryStream memoryStream, string fileName, string contentType, Guid SeekerProfileId);
        Task<bool> insertWorkExperienceAsync(Guid ProfileId, WorkExperienceDto dataRequest);
        public Task AddSkillsToJobSeekerAsync(Guid ProfileID, List<Guid> skills);
        public Task AddQualificatnToJobSeekerAsync(Guid ProfileId, List<Guid> QualiFId);

    }
}
