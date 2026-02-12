

using AutoMapper;
using Domain.Helpers;
using Domain.Models;
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
            CreateMap<JobPost,JobPostsDtos>();
            CreateMap<CompanyUserRequest, CompanyMemberDtos>();
            CreateMap<JobPostRequest, JobPost>().ForMember(dest => dest.JobResponsibilities, opt => opt.Ignore())
    .ForMember(dest => dest.JobPostSkills, opt => opt.Ignore());
            
            CreateMap<JobApplicationsDto, JobApplication>().ReverseMap();
            CreateMap<InterviewSheduleObject,InterviewsheduleDtos>().ReverseMap();
            CreateMap<InterviewsheduleDtos, Interview>();
            CreateMap<Interview, ScheduledInterviewDto>()
    .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job != null ? src.Job.JobTitle : null))
    .ForMember(dest => dest.JobseekerUsername, opt => opt.MapFrom(src => src.Jobseeker != null ? src.Jobseeker.FirstName : null))
    .ForMember(dest => dest.CompanyUserName, opt => opt.MapFrom(src => src.CompanyUser != null ? src.CompanyUser.FirstName : null));

            CreateMap<JobPost, JobPostResponseDtos>();
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


        }
    }
}
