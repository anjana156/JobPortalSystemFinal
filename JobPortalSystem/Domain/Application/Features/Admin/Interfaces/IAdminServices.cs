using Domain.Application.Features.JobSeekers.DTO;
using Domain.Application.Features.Profile.DTO;
using Domain.Models;

namespace Domain.Application.Features.Admin.Interfaces
{
    public interface IAdminServices
    {
        public Task<List<Domain.Models.JobSeeker>> GetJobSeekers();
        public Task<List<JobProviderCompany>> GetCompanies();
        public Task<List<CompanyUser>> GetCompanyUsers();
        public void DeleteById(Guid id);
        public void DeleteCompaniesById(Guid id);
        public int GetCompanyCount();
        public int GetJobProviderCount();
        public int GetJobCount();
        public Task<List<JobPost>> GetJobs(string JobLitle);
        public Task<List<JobPost>> GetJobs();
        public Task<List<JobProviderCompany>> SearchCompanies(string name);


        public Task<SkillDto> AddSkill(SkillDto skillDto);
        public Task<List<SkillDto>> GetSkills();
        public void DeleteBySkillId(Guid id);


        Task<Industry> AddIndustry(Industry industry);
        public Task<List<Industry>> GetIndustries();
        public void DeleteByIndustryId(Guid id);

        Task<JobCategory> AddCategory(JobCategory category);
        public Task<List<JobCategory>> GetCategories();
        public void DeleteByCategoryId(Guid id);


        Task<Location> AddLocation(Location location);
        public Task<List<Location>> GetLocations();
        public void DeleteByLocationId(Guid id);


    }
}
