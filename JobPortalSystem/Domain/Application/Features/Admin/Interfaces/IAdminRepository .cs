using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.Admin.Interfaces
{
    public interface IAdminRepository
    {
        public Task<List<Domain.Models.JobSeeker>> GetJobSeekers();
        public Task<List<JobProviderCompany>> GetCompanies();
        public Task<List<CompanyUser>> GetCompanyUsers();
        public Task<List<JobPost>> GetJobs();
        public void DeleteById(Guid id);
        public void DeleteCompaniesById(Guid id);
        public int GetCompanyCount();
        public int GetJobProviderCount();
        public Task<List<JobPost>> GetJobs(string JobLitle);
        public int GetJobCount();
        public Task<List<JobProviderCompany>> SearchCompanies(string name);

        Task<Skill> AddSkill(Skill skill);
        public Task<List<Skill>> GetSkills();
        public void DeleteBySkillId(Guid id);

        Task<Industry> addIndustry(Industry industry);
        public Task<List<Industry>> GetIndustries();
        public void DeleteByIndustryId(Guid id);

        Task<JobCategory> addCategory(JobCategory category);
        public Task<List<JobCategory>> GetCategories();
        public void DeleteByCategoryId(Guid id);

        Task<Location> addLocation(Location location);
        public Task<List<Location>> GetLocations();
        public void DeleteByLocationId(Guid id);

        
    }
}
