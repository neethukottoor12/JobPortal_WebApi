using AutoMapper;
using Domain.Models;
using Domain.Service.Profile.DTOs;
using Domain.Service.Profile.Intefaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Domain.Service.Profile
{
    public class ProfileService : IProfileservice
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;

        }

        public async Task<bool> UpdateProfileAsync(JobSeekerProfile addProfile)
        {
            await _profileRepository.UpdateProfileAsync(addProfile);
            return true;
        }

        public async Task<ProfileDto> ViewProfileDetailsAsync(Guid JobSeekerId)
        {
            return await _profileRepository.ViewProfileDetailsAsync(JobSeekerId);

        }

        //verify profileID
        public async Task<bool> VerifyProfileIdAsync(Guid JobSeekerProfileId)
        {
            return await _profileRepository.VerifyProfileIdAsync(JobSeekerProfileId);
        }

        //for loading resume
        public async Task<Guid> LoadResume(MemoryStream memoryStream, string title, Guid SeekerProfileId)
        {
            return await _profileRepository.LoadResumeAsync(memoryStream, title, SeekerProfileId);
        }
        //update
        public async Task<Guid> UpdateResume(MemoryStream memoryStream, string title, Guid SeekerProfileId)
        {
            return await _profileRepository.UpdateResumeAsync(memoryStream, title, SeekerProfileId);
        }

        //for loading profile image
        public async Task<Guid> LoadImage(MemoryStream memoryStream, string fileName, string contentType, Guid SeekerProfileId)
        {
            return await _profileRepository.LoadImageAsync(memoryStream, fileName, contentType, SeekerProfileId);
        }

        //for inserting WorkExperience
        public async Task<bool> insertWorkExperience(Guid ProfileId, WorkExperienceDto dataRequest)
        {
            return await _profileRepository.insertWorkExperienceAsync(ProfileId, dataRequest);
        }
        //insert qualification
        public async Task AddSkillsToJobSeekerAsync(Guid profileId, List<Guid> skillId)
        {
            await _profileRepository.AddSkillsToJobSeekerAsync(profileId, skillId);
        }

        //inserting Qualifications
        public async Task AddQualificatnToJobSeekerAsync(Guid ProfileID, List<Guid> qualifIDs)
        {
            await _profileRepository.AddQualificatnToJobSeekerAsync(ProfileID, qualifIDs);
        }
    }
}