using Domain.Models;
using Domain.Service.Admin.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Admin
{
    public class AdminRepository:IAdminRepository
    {
        DbHireMeNowWebApiContext _context;
        public AdminRepository(DbHireMeNowWebApiContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetLocations()
        {
            return await _context.Locations.ToListAsync();

        }
        public async Task<List<Industry>> GetIndustries()
        {
            return await _context.Industries.ToListAsync();
        }
        public async Task<List<JobCategory>> GetJobCategories()
        {
            return await _context.JobCategories.ToListAsync();
        }
        public async Task<List<Skill>> GetSkills()
        {
            return await _context.Skills.ToListAsync();
        }
        public async Task<List<Qualification>> GetQualifications()
        {
            return await _context.Qualifications.ToListAsync();
        }
    }
}
