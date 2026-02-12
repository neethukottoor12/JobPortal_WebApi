using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.Authuser.Interfaces;
using Domain.Service.JobProvider.Interfaces;
using Domain.Service.SignUp.DTOs;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using BCrypt;


namespace Domain.Service.JobProvider
{
    public class JobProviderService : IJobProviderService
    {
        IJobProviderRepository jobProviderRepository;
        IMapper mapper;
        IEMailService emailService;
        IAuthUserRepository authUserRepository;
        public JobProviderService(IJobProviderRepository jobProviderRepository, IMapper mapper, IEMailService eMailService, IAuthUserRepository authUserRepository)
        {
            this.jobProviderRepository = jobProviderRepository;
            this.mapper = mapper;
            emailService = eMailService;
            this.authUserRepository = authUserRepository;
        }
        public async Task CreateSignupRequest(JobProviderSignupRequestDto data)
        {
            var signuprequest = mapper.Map<SignUpRequest>(data);
            
            var signupId = await jobProviderRepository.AddSignupRequest(signuprequest);
            MailRequest mailRequest = new MailRequest()
            {
                Subject = "HireMeNow SignUp Verification",
                Body = "http://localhost:56067/set-password?signupid=" + signupId.ToString(),
                ToEmail = signuprequest.Email
            };
            await emailService.SendEmailAsync(mailRequest);


        }
        public async Task<bool> VerifyEmailAsync(Guid jobProviderSignupRequestId)
        {
            SignUpRequest signUpRequest = await jobProviderRepository.GetSignupRequestByIdAsync(jobProviderSignupRequestId);
            if (signUpRequest != null)
            {
                signUpRequest.Status = Enums.Status.VERIFIED;
                await jobProviderRepository.UpdateSignupRequest(signUpRequest);
                return true;
            }
            return false;
        }
        public async Task CreateJobProvider(Guid jobProviderSignupRequestId, string password)
        {
            try
            {
                SignUpRequest signUpRequest = await jobProviderRepository.GetSignupRequestByIdAsync(jobProviderSignupRequestId);
                AuthUser authUser = new();
                if (signUpRequest.Status == Enums.Status.VERIFIED)
                {
                    authUser.Id=Guid.NewGuid();

                    authUser.UserName = signUpRequest.UserName;
                    Console.WriteLine("About to assign Role");

                    authUser.Role = Enums.Role.JOB_PROVIDER;
                    Console.WriteLine("Role assigned");

                    authUser.FirstName = signUpRequest.FirstName;
                    authUser.LastName = signUpRequest.LastName;
                    authUser.Email = signUpRequest.Email;
                    authUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
                    authUser.Phone = signUpRequest.Phone;
                    authUser = await authUserRepository.AddAuthUserJP(authUser);
                    signUpRequest.Status = Enums.Status.CREATED;
                    await jobProviderRepository.UpdateSignupRequest(signUpRequest);





                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Guid> PostJob(JobPost job, List<string> Responsibilities, List<Guid> Skillids, List<Guid> QualificationIds)
        {
            //add responsibility
            foreach (var item in Responsibilities)
            {
                job.JobResponsibilities.Add(new JobResponsibility
                {
                    Description = item
                });
            }
            // Add skills
            foreach (var skillId in Skillids)
            {
                job.JobPostSkills.Add(new JobPostSkill
                {
                    SkillId = skillId
                });
            }
            foreach(var Qual_Id  in QualificationIds)
            {
                job.JobPostQualifications.Add(new JobPostQualification
                {
                    QualificationId = Qual_Id
                });
            }


            return await jobProviderRepository.Create(job);
        }
        public async Task<List<JobPost>> GetJobs(Guid companyId)
        {

            return await jobProviderRepository.GetJobs(companyId);
        }
        public async Task<List<JobPost>> GetAllJobsByProvider(Guid companyId, Guid jobproviderId)
        {
            return await jobProviderRepository.GetAllJobsByProvider(companyId, jobproviderId);  
        }
        public async Task<JobPost> Update(JobPost job, Guid id)
        {
            return await jobProviderRepository.UpdateAsync(job, id);
        }
        public async Task<bool> DeleteJob(Guid id)
        {
            return await jobProviderRepository.DeleteJob(id);
        }
        public async Task<List<JobProviderCompany>> GetCompany(Guid jobproviderId)
        {
            return await jobProviderRepository.GetCompany(jobproviderId);
        }
        public async Task<List<JobApplication>> GetAllJobApplicants(Guid jobproviderId)
        {
            return await jobProviderRepository.GetAllJobApplicants(jobproviderId);
        }
    }
}
