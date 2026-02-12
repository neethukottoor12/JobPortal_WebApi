using Domain.Service.SignUp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.SignUp.Interfaces
{
    public interface ISignUpRequestService
    {
       Task CreateJobseeker(Guid jobSeekerSignupRequestId, string password);
        Task CreateSignupRequest(JobSeekerSignupRequestDto data);
       Task<bool> VerifyEmailAsync(Guid jobSeekerSignupRequestId);
    }
}
