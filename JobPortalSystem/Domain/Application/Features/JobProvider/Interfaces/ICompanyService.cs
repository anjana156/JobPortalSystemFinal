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
    public interface ICompanyService
    {
        Task<JobProviderCompany> AddCompany(CompanyRegistrationDtos data, Guid UserId);

        GetCompanyDetailsDto GetCompany(Guid companyId);
        Task<JobProviderCompany> UpdateAsync(CompanyUpdateDtos company);
        Task<PagedList<CompanyUser>> memberListing(Guid companyId, CompanyMemberListParam param);
        bool memberDeleteById(Guid id);

        Task<CompanyMemberDtos> addMember(CompanyMemberDtos companyMember, Guid companyId);

    }
}
