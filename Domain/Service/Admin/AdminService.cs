using Domain.Models;
using Domain.Service.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Admin
{
    public class AdminService:IAdminService
    {
        IAdminRepository repo;
        public AdminService(IAdminRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<Location>> GetLocations()
        {
            return await repo.GetLocations();
        }
        public async Task<List<Industry>> GetIndustries()
        {
            return await repo.GetIndustries();

        }
        public async Task<List<JobCategory>> GetJobCategories()
        {
            return await repo.GetJobCategories();
        }
        public async Task<List<Skill>> GetSkills()
        {
            return await repo.GetSkills();
        }
        public async Task<List<Qualification>> GetQualifications()
        {
            return await repo.GetQualifications();
        }
    }
}
