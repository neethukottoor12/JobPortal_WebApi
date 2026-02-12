using AutoMapper;
using Domain.Service.Authuser.Interfaces;
using Domain.Service.SignUp.DTOs;
using Domain.Service.SignUp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;
using Domain.Helpers;

namespace Domain.Service.SignUp
{
    public class SignUpRequestService : ISignUpRequestService
    {

        ISignUpRequestRepository jobSeekerRepository;
        IAuthUserRepository authUserRepository;
        IMapper mapper;
        IEMailService emailService;

        public SignUpRequestService(ISignUpRequestRepository jobSeekerRepository, IAuthUserRepository authUserRepository, IMapper mapper, IEMailService emailService)
        {
            this.jobSeekerRepository = jobSeekerRepository;
            this.authUserRepository = authUserRepository;
            this.mapper = mapper;
            this.emailService = emailService;
        }
        public async Task CreateSignupRequest(JobSeekerSignupRequestDto data)
        {
            var signUpRequest = mapper.Map<SignUpRequest>(data);
            var signUpId = await jobSeekerRepository.AddSignupRequest(signUpRequest);
            MailRequest mailRequest = new MailRequest()
            {
                Subject = "HireMeNow SignUp Verification",
                Body = "http://localhost:4200/set-password?signupid=" + signUpId.ToString(),
                ToEmail = signUpRequest.Email,
            };
            await emailService.SendEmailAsync(mailRequest);

        }
        public async Task<bool> VerifyEmailAsync(Guid jobSeekerSignupRequestId)
        {
            SignUpRequest signUpRequest = await jobSeekerRepository.GetSignupRequestByIdAsync(jobSeekerSignupRequestId);
            if (signUpRequest != null)
            {
                signUpRequest.Status = Enums.Status.VERIFIED;
                jobSeekerRepository.UpdateSignupRequest(signUpRequest);
                return true;
            }
            return false;
        }

        public async Task CreateJobseeker(Guid jobSeekerSignupRequestId, string password)
        {
            try
            {
                SignUpRequest signUpRequest = await jobSeekerRepository.GetSignupRequestByIdAsync(jobSeekerSignupRequestId);
                AuthUser authUser = new();
                if (signUpRequest.Status == Enums.Status.VERIFIED)
                {
                    authUser.Id = Guid.NewGuid();
                    authUser.UserName = signUpRequest.UserName;
                    authUser.FirstName = signUpRequest.FirstName;
                    authUser.LastName = signUpRequest.LastName;
                    authUser.Phone = signUpRequest.Phone;
                    authUser.Email = signUpRequest.Email;
                    authUser.Role = Enums.Role.JOB_SEEKER;
                    authUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
                    authUser = await authUserRepository.AddAuthUser(authUser);
                    signUpRequest.Status = Enums.Status.CREATED;
                    jobSeekerRepository.UpdateSignupRequest(signUpRequest);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}