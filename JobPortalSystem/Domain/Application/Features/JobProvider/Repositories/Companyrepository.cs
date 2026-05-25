using AutoMapper;
using Domain.Application.Features.JobProvider.DTO;
using Domain.Application.Features.JobProvider.Interfaces;
using Domain.Helpers;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.Repositories
{
    public class Companyrepository : ICompanyRepository
    {
        protected JobPortalDbContext _context;
        public IMapper mapper { get; set; }
        public Companyrepository(JobPortalDbContext context, IMapper _mapper)
        {
            _context = context;
            mapper = _mapper;
        }

        public async Task AddCompany(JobProviderCompany data, Guid UserId)
        {
            try
            {
                _context.JobProviderCompanies.AddAsync(data);
                await _context.SaveChangesAsync();
                var CmpanyId = data.Id;
                //AuthUser user = _context.AuthUsers.Where(e => e.Id == UserId).FirstOrDefault();
                Domain.Models.AuthUser user = _context.AuthUsers.Where(e => e.Id == UserId).FirstOrDefault();
                CompanyUser companyUser = new CompanyUser();
                var cmp = _context.CompanyUsers.Where(e => e.Id == UserId).FirstOrDefault();

                cmp.Company = CmpanyId;
                _context.CompanyUsers.Update(cmp);
                await _context.SaveChangesAsync();

                if (cmp == null)
                {
                    companyUser.Id = UserId;
                    companyUser.UserName = user.UserName;
                    companyUser.Email = user.Email;
                    companyUser.FirstName = user.FirstName;
                    companyUser.LastName = user.LastName;
                    companyUser.Phone = user.Phone;
                    companyUser.Role = Enums.Role.COMPANY_USER;
                    companyUser.Company = CmpanyId;
                    _context.CompanyUsers.AddAsync(companyUser);
                    await _context.SaveChangesAsync();
                }



            }
            catch (Exception ex)
            {

            }

        }
        public JobProviderCompany GetCompany(Guid companyId)
        {
            JobProviderCompany company = _context.JobProviderCompanies.Where(e => e.Id == companyId).FirstOrDefault();
            return company;

        }
        public async Task<JobProviderCompany> updateCompanyAsync(JobProviderCompany company)
        {
            var companyToUpdate = await _context.JobProviderCompanies.Where(e => e.Id == company.Id).FirstOrDefaultAsync();
            if (companyToUpdate != null)
            {
                companyToUpdate.LegalName = company.LegalName ?? companyToUpdate.LegalName;
                companyToUpdate.Address = company.Address ?? companyToUpdate.Address;
                //companyToUpdate.Industry = company.Industry == null ? companyToUpdate.Industry : company.Industry;
                companyToUpdate.Email = company.Email ?? companyToUpdate.Email;
                companyToUpdate.Phone = company.Phone == null ? companyToUpdate.Phone : company.Phone;
                companyToUpdate.Website = company.Website == null ? company.Website : companyToUpdate.Website;
                companyToUpdate.Address = company.Address ?? companyToUpdate.Address;
                _context.JobProviderCompanies.Update(companyToUpdate);
                _context.SaveChanges();


            }
            else
            {
                throw new FileNotFoundException("Company Not Found");
            }
            return companyToUpdate;
        }
        public async Task<PagedList<CompanyUser>> memberListing(Guid companyId, CompanyMemberListParam param)
        {
            var query = _context.CompanyUsers.Where(e => e.Company == companyId)
           .AsQueryable();


            return await PagedList<CompanyUser>.CreateAsync(query,
                param.PageNumber, param.PageSize);
        }
        public bool memberDeleteById(Guid id)
        {
            CompanyUser user = _context.CompanyUsers.Where(e => e.Id == id).FirstOrDefault();
            if (user != null)
            {
                _context.CompanyUsers.Remove(user);
                _context.SaveChanges();
                return true;
            }

            else
            {
                return false;
            }
        }

        public async Task<CompanyMemberDtos> AddMemberAsync(CompanyMemberDtos companyMember, Guid companyId)
        {
            companyMember.Company = companyId;
            var companyMemberDtos = mapper.Map<CompanyUser>(companyMember);

            var authUserDtos = mapper.Map<Domain.Models.AuthUser>(companyMember);
            _context.CompanyUsers.Add(companyMemberDtos);
            _context.AuthUsers.Add(authUserDtos);
            await _context.SaveChangesAsync();

            return companyMember;
        }

    }
}
