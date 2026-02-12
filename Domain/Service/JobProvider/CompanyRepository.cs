using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.JobProvider.DTOs;
using Domain.Service.JobProvider.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider
{
    public class CompanyRepository : ICompanyRepository
    {

        private readonly DbHireMeNowWebApiContext _context;
        private readonly IMapper mapper;
        public CompanyRepository(DbHireMeNowWebApiContext context,IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;

        }
        public async Task AddCompany(JobProviderCompany data, Guid UserId)
        {
            try
            {
                _context.JobProviderCompanies.AddAsync(data);
                await _context.SaveChangesAsync();
                var companyid = data.Id;
                AuthUser user = _context.AuthUsers.FirstOrDefault(c => c.Id == UserId);
                var comp = _context.CompanyUsers.FirstOrDefault(c => c.Id == UserId);
                CompanyUser companyUser = new CompanyUser();
                comp.Company = companyid;
                _context.CompanyUsers.Update(comp);
                await _context.SaveChangesAsync();
                if (comp == null)
                {
                    companyUser.Id = UserId;
                    companyUser.UserName = user.UserName;
                    companyUser.Email = user.Email;
                    companyUser.FirstName = user.FirstName;
                    companyUser.LastName = user.LastName;
                    companyUser.Phone = user.Phone;
                    companyUser.Role = Enums.Role.COMPANY_USER;
                    companyUser.Company = companyid;
                    _context.CompanyUsers.AddAsync(companyUser);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JobProviderCompany> GetCompany(Guid Companyid)
        {
            return await _context.JobProviderCompanies.FindAsync(Companyid);
        }
        public async Task<JobProviderCompany> updateCompanyAsync(JobProviderCompany company)
        {
            var companytoupdate = await _context.JobProviderCompanies.Where(c => c.Id == company.Id).FirstOrDefaultAsync();
            if (companytoupdate != null)
            {
                companytoupdate.LegalName = company.LegalName ?? companytoupdate.LegalName;
               companytoupdate.Summary= company.Summary??companytoupdate.Summary;
                
                companytoupdate.Email = company.Email ?? companytoupdate.Email;
                companytoupdate.Phone = company.Phone == null ? companytoupdate.Phone : company.Phone;
                companytoupdate.Website = company.Website == null ? company.Website : companytoupdate.Website;
                companytoupdate.Address = company.Address ?? companytoupdate.Address;
                _context.JobProviderCompanies.Update(companytoupdate);
                await _context.SaveChangesAsync();
                
            }
            else
            {
                throw new FileNotFoundException();
            }
            return companytoupdate;
        }
        public async Task<CompanyMemberDtos> AddMemberAsync(CompanyMemberDtos companyMember, Guid companyId)
        {
            companyMember.Company = companyId;
            var companyuser=mapper.Map<CompanyUser>(companyMember);
            var memberpassword = companyMember.Password;
            companyMember.Password = BCrypt.Net.BCrypt.HashPassword(memberpassword);
            var authuser=mapper.Map<AuthUser>(companyMember);

            _context.CompanyUsers.Add(companyuser);
            _context.AuthUsers.Add(authuser);
            await _context.SaveChangesAsync();
            var companymemberdtos=mapper.Map<CompanyMemberDtos>(companyMember);
            return companymemberdtos;



        }
        public async Task<PagedList<CompanyUser>> memberListing(Guid companyId, CompanyMemberListParam param)
        {
            var query=_context.CompanyUsers.Where(c=>c.Company==companyId).AsQueryable();
            return await PagedList<CompanyUser>.CreateAsync(query, param.PageNumber,param.PageSize);
        }
        public bool memberDeleteById(Guid id)
        {
            var membertodelete=_context.CompanyUsers.Where(u=>u.Id==id).FirstOrDefault();
            var companyauthuser=_context.AuthUsers.Where(u=>u.Id==id).FirstOrDefault();
            if(membertodelete==null&&companyauthuser==null)
                return false;
            if(membertodelete!=null)
            
                _context.CompanyUsers.Remove(membertodelete);
            if (companyauthuser != null)
            
                _context.AuthUsers.Remove(companyauthuser);
            _context.SaveChanges();
            return true;
            
           
        }
    }
}
