using Domain.Helpers;
using Domain.Models;
using Domain.Service.JobProvider.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider.Interfaces
{
    public interface ICompanyService
    {
        Task<JobProviderCompany> AddCompany(CompanyRegistrationDto data, Guid UserId);
        Task<GetCompanyDetailsDto> GetCompany(Guid Companyid);
        Task<JobProviderCompany> UpdateAsync(CompanyUpdateDtos company);
        Task<CompanyMemberDtos> addMember(CompanyMemberDtos companyMember, Guid companyId);
        Task<PagedList<CompanyUser>> memberListing(Guid companyId, CompanyMemberListParam param);
        bool memberDeleteById(Guid id);
    }
}
