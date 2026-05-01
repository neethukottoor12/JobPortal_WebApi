using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Admin.Interfaces
{
    public interface IAdminService
    {
        public Task<List<Location>> GetLocations();
        public Task<List<Industry>> GetIndustries();
        public Task<List<JobCategory>> GetJobCategories();
        public Task<List<Skill>> GetSkills();
        public Task<List<Qualification>> GetQualifications();
    }
}
