using Domain.Models;
using Domain.Service.Profile.DTOs;
using System.IO;

namespace Domain.Service.Profile.Intefaces
{
    public interface IProfileservice
    {
        Task<bool> UpdateProfileAsync(JobSeekerProfile addProfile);
        Task<ProfileDto> ViewProfileDetailsAsync(Guid JobSeekerId);
        Task<bool> VerifyProfileIdAsync(Guid JobSeekerProfileId);
        Task<Guid> LoadResume(MemoryStream memoryStream, string title, Guid SeekerProfileId);
        Task<Guid> UpdateResume(MemoryStream memoryStream, string title, Guid SeekerProfileId);
        Task<Guid> LoadImage(MemoryStream memoryStream, string fileName, string contentType, Guid SeekerProfileId);
        Task<bool> insertWorkExperience(Guid jobSeekerProfileId, WorkExperienceDto dataRequest);

        public Task AddSkillsToJobSeekerAsync(Guid jobSeekerId, List<Guid> skillId);
        public Task AddQualificatnToJobSeekerAsync(Guid ProfileID, List<Guid> qualifIDs);

    }
}