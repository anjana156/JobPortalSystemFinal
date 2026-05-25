using AutoMapper;
using Domain.Application.Features.JobProvider.DTO;
using Domain.Application.Features.JobProvider.Interfaces;
using Domain.Application.Features.JobProvider.Repositories;
using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.Services
{
    public class Companyservice : ICompanyService
    {
        IMapper mapper;
        ICompanyRepository companyRepository;

        public Companyservice(IMapper _mapper, ICompanyRepository _companyRepository)
        {
            mapper = _mapper;
            companyRepository = _companyRepository;
        }


        public async Task<JobProviderCompany> AddCompany(CompanyRegistrationDtos data, Guid UserId)

        {
            var jobproviderCompany = mapper.Map<JobProviderCompany>(data);
            await companyRepository.AddCompany(jobproviderCompany, UserId);


            return jobproviderCompany;

        }



        public GetCompanyDetailsDto GetCompany(Guid companyId)
        {
            var company = companyRepository.GetCompany(companyId);
            var getCompanyDetailsDto = mapper.Map<GetCompanyDetailsDto>(company);
            return getCompanyDetailsDto;
        }
        public async Task<JobProviderCompany> UpdateAsync(CompanyUpdateDtos company)
        {
            JobProviderCompany jobProviderCompany = mapper.Map<JobProviderCompany>(company);
            var jobProviderUpdatedCompany = await companyRepository.updateCompanyAsync(jobProviderCompany);
            //var ComapnyRegistrationDto = mapper.Map<CompanyRegistrationDtos>(jobProviderUpdatedCompany);
            return jobProviderUpdatedCompany;
        }


        public async Task<PagedList<CompanyUser>> memberListing(Guid companyId, CompanyMemberListParam param)
        {
            return await companyRepository.memberListing(companyId, param);
        }
        public bool memberDeleteById(Guid id)
        {
            return companyRepository.memberDeleteById(id);
        }

        public async Task<CompanyMemberDtos> addMember(CompanyMemberDtos companyMember, Guid companyId)
        {
            return await companyRepository.AddMemberAsync(companyMember, companyId);
        }
    }
}
