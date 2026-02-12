using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Login.Interfaces
{
    public interface ILoginRequestRepository
    {
        AuthUser GetUserByEmail(string email);
        Task<AuthUser> GetUserByEmailpassword(string email, string password);
        Task<AuthUser> GetUserByEmailpasswordJp(string email, string password);
    }
}
