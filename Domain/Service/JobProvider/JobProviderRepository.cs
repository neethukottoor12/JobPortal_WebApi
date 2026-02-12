using Domain.Enums;
using Domain.Models;
using Domain.Service.JobProvider.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider
{
    public class JobProviderRepository : IJobProviderRepository
    {
        DbHireMeNowWebApiContext context;
        public JobProviderRepository(DbHireMeNowWebApiContext context)
        {
            this.context = context;
        }
        public async Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobProviderSignupRequestId)
        {
            return await context.SignUpRequests.FindAsync(jobProviderSignupRequestId);
        }
        public async Task UpdateSignupRequest(SignUpRequest signUpRequest)
        {
            context.SignUpRequests.Update(signUpRequest);
            await context.SaveChangesAsync();

        }
        public async Task<Guid> AddSignupRequest(SignUpRequest signUpRequest)
        {
            try
            {
                var emailexist = await context.SignUpRequests.Where(x => x.Email == signUpRequest.Email).FirstOrDefaultAsync();
                if (emailexist != null)
                    throw new InvalidOperationException("Email already exists");

                signUpRequest.Status = Status.PENDING;
                signUpRequest.Id = Guid.NewGuid();
                await context.SignUpRequests.AddAsync(signUpRequest);

                var dbName = context.Database.GetDbConnection().Database;
                Console.WriteLine($"EF is using database: {dbName}");
                await context.SaveChangesAsync();
                return signUpRequest.Id;

            }
            catch
            {
                throw;
            }
                
        }
        public async Task<Guid> Create(JobPost job)
        {
            context.JobPosts.Add(job);
            await context.SaveChangesAsync();
            return job.Id;
        }
        public async Task<List<JobPost>> GetJobs(Guid companyId)
        {
            return await context.JobPosts.Include(j => j.Location)
    .Include(j => j.Industry)
    .Include(j => j.Category)
    .Include(j => j.PostedByNavigation)
.Where(c=>c.CompanyId == companyId).ToListAsync();
        }
        public async Task<List<JobPost>> GetAllJobsByProvider(Guid companyId, Guid jobproviderId)
        {
            return await context.JobPosts.Where(c=>c.CompanyId==companyId&&c.PostedBy==jobproviderId).ToListAsync();
        }
        public async Task<JobPost> UpdateAsync(JobPost Updatedjob, Guid id)
        {

            var jobToUpdate=await context.JobPosts.Include(j => j.JobResponsibilities)
            .Include(j => j.JobPostSkills)
            .Include(j=>j.JobPostQualifications)
            .Where(j=>j.Id==id).FirstOrDefaultAsync();
            if (jobToUpdate!=null)
            {
                // Remove old responsibilities
                context.JobResponsibilities.RemoveRange(jobToUpdate.JobResponsibilities);

                // Remove old skills
                context.JobPostSkills.RemoveRange(jobToUpdate.JobPostSkills);

                //Remove old Qualifications
                context.JobPostQualifications.RemoveRange(jobToUpdate.JobPostQualifications);

                foreach (var r in Updatedjob.JobResponsibilities)
                {
                    jobToUpdate.JobResponsibilities.Add(new JobResponsibility
                    {
                        Id = Guid.NewGuid(),
                        Description=r.Description,
                       
                        JobPost = jobToUpdate.Id
                    });
                }
                foreach (var skill in Updatedjob.JobPostSkills)
                {
                    jobToUpdate.JobPostSkills.Add(new JobPostSkill
                    {
                        JobPostId = jobToUpdate.Id,
                        SkillId = skill.SkillId
                    });
                }
                foreach (var Qualification in Updatedjob.JobPostQualifications)
                {
                    jobToUpdate.JobPostQualifications.Add(new JobPostQualification
                    {
                        JobPostId = jobToUpdate.Id,
                        QualificationId = Qualification.QualificationId
                    });
                }





                jobToUpdate.JobTitle = Updatedjob.JobTitle;
                jobToUpdate.JobSummary = Updatedjob.JobSummary;
                jobToUpdate.LocationId = Updatedjob.LocationId;
                jobToUpdate.CompanyId = Updatedjob.CompanyId;
                jobToUpdate.CategoryId = Updatedjob.CategoryId;
                jobToUpdate.IndustryId = Updatedjob.IndustryId;

                jobToUpdate.PostedDate = Updatedjob.PostedDate;
                context.JobPosts.Update(jobToUpdate);
              

                    await context.SaveChangesAsync();
               

            }
            else
            {
                throw new FileNotFoundException("Company Not Found");
            }
            return jobToUpdate;


        }
        public async Task<bool> DeleteJob(Guid id)
        {
            var jobToDelete=await context.JobPosts.Include(j => j.JobResponsibilities)
             .Include(j => j.JobPostSkills)
                .Where(j=>j.Id == id).FirstOrDefaultAsync();  
            if(jobToDelete!=null)
            {
                context.JobResponsibilities.RemoveRange(jobToDelete.JobResponsibilities);
                context.JobPostSkills.RemoveRange(jobToDelete.JobPostSkills);

                context.JobPosts.Remove(jobToDelete);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<JobProviderCompany>> GetCompany(Guid jobproviderId)
        {
            var companyUser = await context.CompanyUsers
           .Where(e => e.Id == jobproviderId)
           .FirstOrDefaultAsync();

            Guid? companyId = companyUser.Company;

            

            var companies = await context.JobProviderCompanies
      .Where(e => e.Id == companyId)
      .ToListAsync();


            return companies;

        }
        public async Task<List<JobApplication>> GetAllJobApplicants(Guid jobproviderId)
        {
            var companuser = await context.CompanyUsers.Where(c => c.Id == jobproviderId).FirstOrDefaultAsync();

            Guid? companyid = companuser.Company;
            var jobposts=await context.JobPosts.Where(j=>j.CompanyId == companyid).ToListAsync();
            var jobpostIds=jobposts.Select(j=>j.Id).ToList();
            var jobapplications = await context.JobApplications
                .Include(ja => ja.Resume)
                .Include(ja => ja.Seeker)
                .Include(ja => ja.JobPost)
                .Where(ja => jobpostIds.Contains(ja.JobPostId))
                .ToListAsync();
            return jobapplications;

        }

    }
}
