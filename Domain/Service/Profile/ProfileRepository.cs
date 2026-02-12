using AutoMapper;
using Domain.Models;
using Domain.Service.Profile.DTOs;
using Domain.Service.Profile.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DbHireMeNowWebApiContext _context;
        private readonly IMapper _mapper;
        public ProfileRepository(DbHireMeNowWebApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task UpdateProfileAsync(JobSeekerProfile profile)
        {
            var existingProfile = await _context.JobSeekerProfiles.FirstOrDefaultAsync(p => p.JobSeekerId == profile.JobSeekerId);

            if (existingProfile != null)
            {
                existingProfile.ProfileName = profile.ProfileName;
                existingProfile.ProfileSummary = profile.ProfileSummary;
                existingProfile.JobSeekerId = profile.JobSeekerId;
                existingProfile.LocationId = profile.LocationId;
                await _context.SaveChangesAsync();
            }
        }


        public async Task<ProfileDto> ViewProfileDetailsAsync(Guid jobSeekerId)
        {
            //var getProfile=await _context.JobSeekerProfiles.FindAsync(JobSeekerId);
            var getProfile = await _context.JobSeekerProfiles.FirstOrDefaultAsync(p => p.JobSeekerId == jobSeekerId);

            var profile = _mapper.Map<ProfileDto>(getProfile);
            return profile;
        }

        //verify profileid
        public async Task<bool> VerifyProfileIdAsync(Guid ProfileId)
        {
            var profile = await _context.JobSeekerProfiles.AnyAsync(j => j.Id == ProfileId);
            if (profile != null) return true;
            return false;

        }

        //Load Resume
        public async Task<Guid> LoadResumeAsync(MemoryStream memoryStream, string title, Guid seekerProfileId)
        {
            var resume = new Resume
            {
                Id = Guid.NewGuid(), // Optional if your model already sets it
                JobSeekerProfileId = seekerProfileId,
                Title = title,
                File = memoryStream.ToArray()
            };

            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();
            var profile = await _context.JobSeekerProfiles.FindAsync(seekerProfileId);
            if (profile != null)
            {
                //profile.ResumeId = resume.Id;
                _context.JobSeekerProfiles.Update(profile);
                await _context.SaveChangesAsync();
            }
            return resume.Id; // Return the new Resume ID, not seekerProfileId
        }

        //Update Resume
        public async Task<Guid> UpdateResumeAsync(MemoryStream memoryStream, string title, Guid seekerProfileId)
        {
            // Try to find an existing resume linked to this profile
            var existingResume = await _context.Resumes
                .FirstOrDefaultAsync(r => r.JobSeekerProfileId == seekerProfileId);

            Resume resume;

            if (existingResume != null)
            {
                // Update existing resume
                existingResume.Title = title;
                existingResume.File = memoryStream.ToArray();
                _context.Resumes.Update(existingResume);
                resume = existingResume;
            }
            else
            {
                // Create new resume
                resume = new Resume
                {
                    Id = Guid.NewGuid(),
                    JobSeekerProfileId = seekerProfileId,
                    Title = title,
                    File = memoryStream.ToArray()
                };
                await _context.Resumes.AddAsync(resume);
            }

            await _context.SaveChangesAsync();

            // Update JobSeekerProfile to reference the resume
            var profile = await _context.JobSeekerProfiles
                .FirstOrDefaultAsync(p => p.Id == seekerProfileId);

            if (profile != null)
            {
               // profile.ResumeId = resume.Id;
                _context.JobSeekerProfiles.Update(profile);
                await _context.SaveChangesAsync();
            }

            return resume.Id;
        }

        //Upload ProfileImage
        public async Task<Guid> LoadImageAsync(MemoryStream memoryStream, string fileName, string contentType, Guid seekerProfileId)
        {
            // Load the profile with its image
            var profile = await _context.JobSeekerProfiles
                                        .Include(p => p.JobSeekerImage)
                                        .FirstOrDefaultAsync(p => p.Id == seekerProfileId);

            if (profile == null)
                throw new Exception("Profile not found");

            if (profile.JobSeekerImage == null)
            {
                // Insert new image
                var jobSeekerImage = new JobSeekerImage
                {
                    Id = Guid.NewGuid(),
                    FileName = fileName,
                    ContentType = contentType,
                    ImageData = memoryStream.ToArray(),
                    JobSeekerProfileId = seekerProfileId
                };

                profile.JobSeekerImage = jobSeekerImage;
                _context.JobSeekerImages.Add(jobSeekerImage);
                await _context.SaveChangesAsync();

                return jobSeekerImage.Id;
            }
            else
            {
                // Update existing image
                profile.JobSeekerImage.FileName = fileName;
                profile.JobSeekerImage.ContentType = contentType;
                profile.JobSeekerImage.ImageData = memoryStream.ToArray();

                _context.JobSeekerImages.Update(profile.JobSeekerImage);
                await _context.SaveChangesAsync();

                return profile.JobSeekerImage.Id;
            }
        }



        //Add WorkExperience
        public async Task<bool> insertWorkExperienceAsync(Guid ProfileId, WorkExperienceDto dataRequest)
        {
            WorkExperience workExp = new WorkExperience()
            {
                Id = Guid.NewGuid(),
                JobSeekerProfileId = ProfileId,
                JobTitle = dataRequest.JobTitle,
                CompanyName = dataRequest.CompanyName,
                Summary = dataRequest.Summary,
                ServiceStart = dataRequest.ServiceStart,
                ServiceEnd = dataRequest.ServiceEnd,
            };
            if (workExp != null)
            {
                 _context.WorkExperiences.AddAsync(workExp);
                await _context.SaveChangesAsync();
                return true;
            }
            
               
            return false;
        }
        //Skills
        public async Task AddSkillsToJobSeekerAsync(Guid jobSeekerProfileId, List<Guid> skillIds)
        {
            var seekerprofile = await _context.JobSeekerProfiles
                .Include(p => p.ProfileSkill)
                .FirstOrDefaultAsync(p => p.Id == jobSeekerProfileId);

            if (seekerprofile == null)
                throw new InvalidOperationException();

            foreach (var skillId in skillIds)
            {
                bool alreadyExists = seekerprofile.ProfileSkill
                    .Any(ps => ps.SkillId == skillId);

                if (!alreadyExists)
                {
                    seekerprofile.ProfileSkill.Add(new ProfileSkill
                    {
                        JobSeekerProfileId = jobSeekerProfileId,
                        SkillId = skillId
                    });
                }
            }

            await _context.SaveChangesAsync();
        }



        //ProfileQualification
        public async Task AddQualificatnToJobSeekerAsync(Guid profileId, List<Guid> qualifIDs)
        {
            var seekerprofile = await _context.JobSeekerProfiles.Include(p => p.ProfileQualification).FirstOrDefaultAsync(p => p.Id == profileId);

            if (seekerprofile == null)
                throw new InvalidOperationException($"JobSeekerProfile with ID {profileId} not found.");

            foreach (var qualifId in qualifIDs)
            {
                bool alreadyExists = seekerprofile.ProfileQualification
                    .Any(pq => pq.QualificationId == qualifId);

                if (!alreadyExists)
                {
                    seekerprofile.ProfileQualification.Add(new ProfileQualification
                    {
                        JobSeekerProfileId = profileId,
                        QualificationId = qualifId
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

    }
}