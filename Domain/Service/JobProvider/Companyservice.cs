using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.JobProvider.DTOs;
using Domain.Service.JobProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.JobProvider
{
    public class Companyservice : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper mapper;
        public Companyservice(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            this.mapper = mapper;
        }
        public async Task<JobProviderCompany> AddCompany(CompanyRegistrationDto data, Guid UserId)
        {
            var jobprovidecompany = mapper.Map<JobProviderCompany>(data);
            await _companyRepository.AddCompany(jobprovidecompany, UserId);
            return jobprovidecompany;
        }
        public async Task<GetCompanyDetailsDto> GetCompany(Guid Companyid)
        {
            var companydetails=await _companyRepository.GetCompany(Companyid);
            return mapper.Map<GetCompanyDetailsDto>(companydetails);
        }
        public async Task<JobProviderCompany> UpdateAsync(CompanyUpdateDtos company)
        {
            var jobprovidercompany=mapper.Map<JobProviderCompany>(company);
            return await _companyRepository.updateCompanyAsync(jobprovidercompany);
        }
        public async Task<CompanyMemberDtos> addMember(CompanyMemberDtos companyMember, Guid companyId)
        {
            return await _companyRepository.AddMemberAsync(companyMember, companyId);
        }
        public async Task<PagedList<CompanyUser>> memberListing(Guid companyId, CompanyMemberListParam param)
        {
            return await _companyRepository.memberListing(companyId, param);
        }
        public bool memberDeleteById(Guid id)
        {
            return _companyRepository.memberDeleteById(id);
        }
    }
}
