using Domain.Helpers;
using Domain.Models;
using Domain.Service.JobProvider.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.Interfaces
{
    public interface ICompanyRepository
    {
        Task AddCompany(JobProviderCompany data, Guid UserId);
        Task<JobProviderCompany> GetCompany(Guid Companyid);
        Task<JobProviderCompany> updateCompanyAsync(JobProviderCompany company);
        Task<CompanyMemberDtos> AddMemberAsync(CompanyMemberDtos companyMember, Guid companyId);
        Task<PagedList<CompanyUser>> memberListing(Guid companyId, CompanyMemberListParam param);
        bool memberDeleteById(Guid id);
    }
}
