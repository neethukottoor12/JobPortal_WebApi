using Domain.Service.Login.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Login.Interfaces
{
    public interface ILoginRequestService
    {
        Task<JobSeekerLoginDto> login(string email, string password);
        Task<JobProviderLoginDto> loginJP(string email, string password);
    }
}
