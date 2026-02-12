using Domain.Enums;
using Domain.Models;
using Domain.Service.SignUp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.SignUp
{
    public class SignUpRequestRepository:ISignUpRequestRepository
    {
        protected readonly DbHireMeNowWebApiContext _context;
        public SignUpRequestRepository(DbHireMeNowWebApiContext context)
        {
            _context = context;
        }
        public async Task AddJobSeekerAsync(Models.JobSeeker jobseeker)
        {
            jobseeker.Id = Guid.NewGuid();
            _context.JobSeekers.Add(jobseeker);
            await _context.SaveChangesAsync();

        }
        public async Task<Guid> AddSignupRequest(SignUpRequest signUpRequest)
        {
            try
            {
                var emailexist = await _context.SignUpRequests.Where(x => x.Email == signUpRequest.Email).FirstOrDefaultAsync();
                if (emailexist != null)
                    throw new InvalidOperationException("Email already exists");

                signUpRequest.Status = Status.PENDING;
                await _context.SignUpRequests.AddAsync(signUpRequest);
                var dbName = _context.Database.GetDbConnection().Database;
                Console.WriteLine($"EF is using database: {dbName}");
                await _context.SaveChangesAsync();
                return signUpRequest.Id;
            }
            catch
            {
                throw;
            }
        }
        
        public async Task UpdateJobSeekerAsync(Models.JobSeeker jobseeker)
        {
            //jobseeker.Id = Guid.NewGuid();
            _context.JobSeekers.Update(jobseeker);
            await _context.SaveChangesAsync();

        }
        public async Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobSeekerSignupRequestId)
        {
            return await _context.SignUpRequests.FindAsync(jobSeekerSignupRequestId);
        }

        public async Task UpdateSignupRequest(SignUpRequest signUpRequest)
        {
            _context.SignUpRequests.Update(signUpRequest);
            await _context.SaveChangesAsync();
        }
    }
}
