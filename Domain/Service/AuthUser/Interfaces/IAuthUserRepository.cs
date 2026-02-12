using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Authuser.Interfaces
{
    public interface IAuthUserRepository
    {
        Task<AuthUser> AddAuthUser(AuthUser authUser);


        string? CreateToken(AuthUser user);
        Task AddUserConnectionId(string email, string ConnectionId);
        public Task<AuthUser> AddAuthUserJP(AuthUser authUser);
        Task<CompanyUser> GetUser(Guid userid);

    }
}
