using Domain.Application.Features.JobProvider.DTO;
using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.Interfaces
{
    public interface ICompanyRepository
    {
        Task AddCompany(JobProviderCompany data, Guid UserId);
        JobProviderCompany GetCompany(Guid companyId);
        Task<JobProviderCompany> updateCompanyAsync(JobProviderCompany company);
        Task<PagedList<CompanyUser>> memberListing(Guid companyId, CompanyMemberListParam param);
        bool memberDeleteById(Guid id);

        Task<CompanyMemberDtos> AddMemberAsync(CompanyMemberDtos companyMember, Guid companyId);

    }
}
