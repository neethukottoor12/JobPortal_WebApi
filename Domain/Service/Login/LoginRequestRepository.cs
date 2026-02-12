using Domain.Models;
using Domain.Service.Login.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Login
{
    public class LoginRequestRepository : ILoginRequestRepository
    {
        protected readonly DbHireMeNowWebApiContext _context;
        public LoginRequestRepository(DbHireMeNowWebApiContext dbContext)
        {
            _context = dbContext;
        }
        public AuthUser GetUserByEmail(string email)
        {
            var user = _context.AuthUsers.FirstOrDefault(e => e.Email == email);
            return user;
        }
        public async Task<AuthUser> GetUserByEmailpassword(string email, string password)
        {
            var user = await _context.AuthUsers.FirstOrDefaultAsync(e => e.Email == email&&e.Role==Enums.Role.JOB_SEEKER);
            if (user == null)
                return null;
            return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;



        }
        public async Task<AuthUser> GetUserByEmailpasswordJp(string email, string password)
        {
            var user = await _context.AuthUsers.FirstOrDefaultAsync(e => e.Email == email&&e.Role==Enums.Role.JOB_PROVIDER);
            if (user == null)
                return null;
            return  BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;


        }
    }
}
