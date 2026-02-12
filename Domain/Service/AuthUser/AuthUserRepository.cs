using AutoMapper;
using Domain.Models;
using Domain.Service.Authuser.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Domain.Service.Authuser
{
    public class AuthUserRepository : IAuthUserRepository
    {
        protected readonly DbHireMeNowWebApiContext _context;
        IMapper mapper;
        private readonly IConfiguration _configuration;
        public AuthUserRepository(DbHireMeNowWebApiContext dbContext, IMapper _mapper, IConfiguration configuration)
        {
            _context = dbContext;
            mapper = _mapper;
            _configuration = configuration;
        }



        public string? CreateToken(AuthUser user)
        {
            if (user == null)
            {
                // Handle the case where the user object is null, e.g., by throwing an exception or returning null.
                throw new ArgumentNullException(nameof(user), "User object cannot be null.");
            }
            string tokenSecret = _configuration.GetSection("AuthSettings:Token").Value;
            if (string.IsNullOrEmpty(tokenSecret))
            {
                // Handle the case where the token secret is missing or empty, e.g., by throwing an exception or returning null.
                throw new InvalidOperationException("Token secret is missing or empty in configuration.");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AuthSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        public async Task AddUserConnectionId(string email, string ConnectionId)
        {
            var userToUpdate = _context.AuthUsers.Where(e => e.Email == email).FirstOrDefault();
            if (userToUpdate != null)
            {
                userToUpdate.ConnectionId = ConnectionId;
                userToUpdate.OnlineStatus = true;
                //userToUpdate.LastActive=DateTime.Now;
                _context.AuthUsers.Update(userToUpdate);
                _context.SaveChanges();
            }
        }
        public async Task<AuthUser> AddAuthUser(AuthUser authUser)
        {
            authUser.Role = Enums.Role.JOB_SEEKER;
            await _context.AuthUsers.AddAsync(authUser);
            await _context.SaveChangesAsync();
            Models.JobSeeker jobSeeker = mapper.Map<Models.JobSeeker>(authUser);
            jobSeeker.SystemUserId = authUser.Id;
            await _context.JobSeekers.AddAsync(jobSeeker);
            await _context.SaveChangesAsync();
            var SeekerId = jobSeeker.Id;
            JobSeekerProfile seekerProfile = new()
            {
                Id = Guid.NewGuid(),
                JobSeekerId = SeekerId
            };

            await _context.JobSeekerProfiles.AddAsync(seekerProfile);
            await _context.SaveChangesAsync(); 
            return authUser;

        }

        
        public async Task<AuthUser> AddAuthUserJP(AuthUser authUser)
        {
            authUser.Role= Enums.Role.JOB_PROVIDER;
            await _context.AuthUsers.AddAsync(authUser);
            Models.CompanyUser jobprovider = mapper.Map<Models.CompanyUser>(authUser);
            Console.WriteLine(jobprovider.Role.GetType().Name);
            Console.WriteLine(jobprovider.Role);

            await _context.CompanyUsers.AddAsync(jobprovider);

            await _context.SaveChangesAsync();
            return authUser;

        }
        public async Task<CompanyUser> GetUser(Guid userid)
        {
            return await _context.CompanyUsers.FirstOrDefaultAsync(c => c.Id == userid);
        }
    }
}
