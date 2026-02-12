using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.SignUp.Interfaces
{
    public interface ISignUpRequestRepository
    {
        Task<Guid> AddSignupRequest(SignUpRequest signUpRequest);
        Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobSeekerSignupRequestId);
        Task UpdateSignupRequest(SignUpRequest signUpRequest);
    }
}
