

using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.Admin.DTOs;
using Domain.Service.Authuser.DTOs;
using Domain.Service.Job.DTOs;
using Domain.Service.JobProvider.DTOs;
using Domain.Service.Login.DTOs;
using Domain.Service.Profile.DTOs;
using Domain.Service.SignUp.DTOs;
using Domain.Service.SignUp.Interfaces;
using JobPortal_Project.API.JobProvider.RequestObjects;
using JobPortal_Project.API.JobSeeker.RequestObjects;
using System;


namespace HireMeNow_WebApi.Extensions
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<JobProviderSignupRequestDto, JobProviderSignupRequest>().ReverseMap();
            CreateMap<SignUpRequest, JobProviderSignupRequestDto>().ReverseMap();
            CreateMap<AuthUser, CompanyUser>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => (Domain.Enums.Role)src.Role));


            CreateMap<SystemUser, JobProviderLoginDto>();



            CreateMap<AuthUser, SignUpRequest>().ReverseMap();

            CreateMap<AuthUser, JobProviderLoginDto>();
            CreateMap<AddCompanyRequest, CompanyRegistrationDto>();
            CreateMap<CompanyRegistrationDto, JobProviderCompany>().ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.IndustryId));
            ;
            CreateMap<JobProviderCompany, GetCompanyDetailsDto>();
            CreateMap<CompanyupdateRequest, CompanyUpdateDtos>();
            CreateMap<CompanyUpdateDtos, JobProviderCompany>();
            CreateMap<CompanyUser,CompanyMemberDtos>().ReverseMap();
            CreateMap<CompanyMemberDtos, AuthUser>();
            CreateMap<CompanyUser,CompanyMemberListDtos>();
            CreateMap<JobPost, JobPostsDtos>()
    .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.Location.Name))
    .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name))
    .ForMember(d => d.IndustryName, opt => opt.MapFrom(s => s.Industry.Name))
    .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Company.LegalName))
    .ForMember(d => d.Responsibilities, opt => opt.MapFrom(s => s.JobResponsibilities.Select(r => r.Description)))
    .ForMember(d => d.Skills, opt => opt.MapFrom(s => s.JobPostSkills.Select(ps => ps.Skill.Name)))
    .ForMember(d => d.Qualifications, opt => opt.MapFrom(s => s.JobPostQualifications.Select(pq => pq.Qualification.Name)));

            CreateMap<JobPost,UpdateJobPostDtos>().ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.Location.Name))
    .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name))
    .ForMember(d => d.IndustryName, opt => opt.MapFrom(s => s.Industry.Name))
    .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Company.LegalName))
    .ForMember(d => d.Responsibilities, opt => opt.MapFrom(s => s.JobResponsibilities.Select(r => new ResponsibilityDto
    {
        Id = r.Id,
        Description = r.Description
    })))
    .ForMember(d => d.Skills, opt => opt.MapFrom(s => s.JobPostSkills.Select(ps => new SkillListDtos
    {
        Id = ps.SkillId,
        Name = ps.Skill.Name
    })))
    .ForMember(d => d.Qualifications, opt => opt.MapFrom(s => s.JobPostQualifications.Select(pq => new QualificationListDtos
    {
        Id = pq.QualificationId,
        Name = pq.Qualification.Name
    })));

            CreateMap<CompanyUserRequest, CompanyMemberDtos>();
            CreateMap<JobPostRequest, JobPost>().ForMember(dest => dest.JobResponsibilities, opt => opt.Ignore())
    .ForMember(dest => dest.JobPostSkills, opt => opt.Ignore());

            //CreateMap<JobApplicationsDto, JobApplication>().ReverseMap();
            CreateMap<JobApplication, JobApplicationsDto>()
    .ForMember(d => d.ApplicationId, opt => opt.MapFrom(s => s.Id))
    .ForMember(d => d.ApplicantId, opt => opt.MapFrom(s => s.Applicant))
    .ForMember(d => d.JobSeekerProfileId, opt => opt.MapFrom(s => s.JobSeekerProfileId))
    .ForMember(d => d.ApplicantName, opt => opt.MapFrom(s =>
        s.Seeker.FirstName + " " + s.Seeker.LastName))
    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Seeker.Email))
    .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Seeker.Phone))
    .ForMember(d => d.ProfileImageBase64, opt => opt.MapFrom(s =>
    s.JobSeekerProfile.JobSeekerImage != null
        ? Convert.ToBase64String(s.JobSeekerProfile.JobSeekerImage.ImageData)
        : null))
    .ForMember(d => d.Qualifications, opt => opt.MapFrom(s =>
        s.JobSeekerProfile.ProfileQualification.Select(q => q.Qualification.Name)))
    .ForMember(d => d.TotalYearsOfExperience, opt => opt.MapFrom(s =>(int)Math.Floor(
        s.JobSeekerProfile.WorkExperiences.Sum(w =>
            (w.ServiceEnd - w.ServiceStart).TotalDays / 365))))
    .ForMember(d => d.JobTitle, opt => opt.MapFrom(s => s.JobTitle))
    .ForMember(d => d.JobPostId, opt => opt.MapFrom(s => s.JobPostId))
    .ForMember(d => d.AppliedDate, opt => opt.MapFrom(s => s.AppliedDate));

            CreateMap<InterviewSheduleObject,InterviewsheduleDtos>().ReverseMap();
            CreateMap<InterviewsheduleDtos, Interview>();
            CreateMap<Interview, ScheduledInterviewDto>()
    .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job != null ? src.Job.JobTitle : null))
    .ForMember(dest => dest.JobseekerUsername, opt => opt.MapFrom(src => src.Jobseeker != null ? src.Jobseeker.FirstName : null))
    .ForMember(dest => dest.CompanyUserName, opt => opt.MapFrom(src => src.CompanyUser != null ? src.CompanyUser.FirstName : null));

            CreateMap<JobPost, JobPostResponseDtos>();
            CreateMap<JobProviderCompany, JobProviderCompanyDTO>();
            CreateMap<JobApplication, ApplicantDetailsDto>()
    // Application Info
    .ForMember(d => d.ApplicationId, opt => opt.MapFrom(s => s.Id))
    .ForMember(d => d.AppliedDate, opt => opt.MapFrom(s => s.AppliedDate))

    // Job Info
    .ForMember(d => d.JobPostId, opt => opt.MapFrom(s => s.JobPostId))
    .ForMember(d => d.JobTitle, opt => opt.MapFrom(s => s.JobTitle))
    .ForMember(d => d.JobSummary, opt => opt.MapFrom(s => s.JobSummary))
    .ForMember(d => d.CompanyId, opt => opt.MapFrom(s => s.CompanyId))
    .ForMember(d => d.JobLocationId, opt => opt.MapFrom(s => s.LocationId))

    // Applicant Basic Info
    .ForMember(d => d.ApplicantId, opt => opt.MapFrom(s => s.Seeker.Id))
    .ForMember(d => d.ApplicantName, opt => opt.MapFrom(s => s.Seeker.FirstName + " " + s.Seeker.LastName))
    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Seeker.Email))
    .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Seeker.Phone))

    // Profile Info
    .ForMember(d => d.JobSeekerProfileId, opt => opt.MapFrom(s => s.JobSeekerProfile.Id))
    .ForMember(d => d.ProfileName, opt => opt.MapFrom(s => s.JobSeekerProfile.ProfileName))
    .ForMember(d => d.ProfileSummary, opt => opt.MapFrom(s => s.JobSeekerProfile.ProfileSummary))
    .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.JobSeekerProfile.Location.Name))

    // Profile Image
    .ForMember(d => d.ProfileImageBase64, opt => opt.MapFrom(s =>
        s.JobSeekerProfile.JobSeekerImage != null &&
        s.JobSeekerProfile.JobSeekerImage.ImageData != null
            ? Convert.ToBase64String(s.JobSeekerProfile.JobSeekerImage.ImageData)
            : null
    ))

    // Resume
    .ForMember(d => d.ResumeFileName, opt => opt.MapFrom(s => s.Resume.Title))
    .ForMember(d => d.ResumeBase64, opt => opt.MapFrom(s =>
        s.Resume != null && s.Resume.File != null
            ? Convert.ToBase64String(s.Resume.File)
            : null
    ))

    // Qualifications
    .ForMember(d => d.Qualifications, opt => opt.MapFrom(s =>
        s.JobSeekerProfile.ProfileQualification
            .Select(q => q.Qualification.Name)
            .ToList()
    ))

    // Skills
    .ForMember(d => d.Skills, opt => opt.MapFrom(s =>
        s.JobSeekerProfile.ProfileSkill
            .Select(sk => sk.Skill.Name)
            .ToList()
    ))

    // Total Experience
    .ForMember(d => d.TotalYearsOfExperience, opt => opt.MapFrom(s =>
        s.JobSeekerProfile.WorkExperiences
            .Sum(w => (w.ServiceEnd - w.ServiceStart).TotalDays / 365.0)
    ))

    // Work Experience List
    .ForMember(d => d.WorkExperiences, opt => opt.MapFrom(s => s.JobSeekerProfile.WorkExperiences));

            CreateMap<WorkExperience, ExperienceDto>()
    .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.CompanyName))
    .ForMember(d => d.JobTitle, opt => opt.MapFrom(s => s.JobTitle))
    .ForMember(d => d.ServiceStart, opt => opt.MapFrom(s => s.ServiceStart))
    .ForMember(d => d.ServiceEnd, opt => opt.MapFrom(s => s.ServiceEnd));



            //Jobseeker
            CreateMap<JobSeekerSignupRequestDto, SignUpRequest>().ReverseMap();
            CreateMap<JobSeekerSignupRequest, JobSeekerSignupRequestDto>().ReverseMap();
            CreateMap<SignUpRequest, SystemUser>().ReverseMap();
            CreateMap<AuthUser, Domain.Models.JobSeeker>().ReverseMap();
            CreateMap<AuthUser, JobSeekerLoginDto>();
            CreateMap<ProfileDto, JobSeekerProfile>().ReverseMap();
            CreateMap<JobPost, JobPostDto>();
            CreateMap<JobPost, JobSearchDto>();
            CreateMap<JobApplication, JobPostDto>();

            //Admin
            CreateMap<Location,LocationDTO>();
            CreateMap<Industry,IndustryDTOs>();
            CreateMap<JobCategory,CategoryDTOs>();
            CreateMap<Skill, SkillDTOs>();
            CreateMap<Qualification,QualificationDTOs>();

        }
    }
}
