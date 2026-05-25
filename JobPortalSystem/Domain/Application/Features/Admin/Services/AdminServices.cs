using AutoMapper;
using Domain.Application.Features.Admin.Interfaces;
using Domain.Application.Features.JobSeekers.DTO;
using Domain.Application.Features.Profile.DTO;
using Domain.Models;

namespace Domain.Application.Features.Admin.Services
{
    public class AdminServices : IAdminServices
    {
        IAdminRepository _adminRepository;
        IMapper _mapper;

        public AdminServices(IAdminRepository adminRepository, IMapper mapper)


        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public async Task<List<Domain.Models.JobSeeker>> GetJobSeekers()
        {
            return await _adminRepository.GetJobSeekers();
        }

        public async Task<List<JobProviderCompany>> GetCompanies()
        {
            return await _adminRepository.GetCompanies();
        }

        public async Task<List<CompanyUser>> GetCompanyUsers()
        {
            return await _adminRepository.GetCompanyUsers();
        }
        
        public async Task<List<SkillDto>> GetSkills()
        {
            var skills = await _adminRepository.GetSkills();
            return _mapper.Map<List<SkillDto>>(skills);
        }

        public async Task<List<Industry>> GetIndustries()
        {
            return await _adminRepository.GetIndustries();
        }

        public async Task<List<Location>> GetLocations()
        {
            return await _adminRepository.GetLocations();
        }

        public async Task<List<JobCategory>> GetCategories()
        {
            return await _adminRepository.GetCategories();
        }

        public async Task<List<JobPost>> GetJobs()
        {
            return await _adminRepository.GetJobs();
        }
        public void DeleteById(Guid id)
        {
            _adminRepository.DeleteById(id);
        }

        public void DeleteByLocationId(Guid id)
        {
            _adminRepository.DeleteByLocationId(id);
        }

        public void DeleteByCategoryId(Guid id)
        {
            _adminRepository.DeleteByCategoryId(id);
        }
        public void DeleteCompaniesById(Guid id)
        {
            _adminRepository.DeleteCompaniesById(id);
        }
        public void DeleteByIndustryId(Guid id)
        {
            _adminRepository.DeleteByIndustryId(id);
        }

        public int GetCompanyCount()
        {
            return _adminRepository.GetCompanyCount();
        }

        public int GetJobProviderCount()
        {
            return _adminRepository.GetJobProviderCount();
        }

        public int GetJobCount()
        {
            return _adminRepository.GetJobCount();
        }
        public async Task<List<JobPost>> GetJobs(string JobLitle)
        {

            var jobs = await _adminRepository.GetJobs(JobLitle);

            return jobs;


        }


        public Task<List<JobProviderCompany>> SearchCompanies(string name)
        {
            return _adminRepository.SearchCompanies(name);
        }

        public async Task<SkillDto> AddSkill(SkillDto skillDto)
        {
            var skill = _mapper.Map<Skill>(skillDto);

            var result = await _adminRepository.AddSkill(skill);

            return _mapper.Map<SkillDto>(result);
        }


        public void DeleteBySkillId(Guid id)
        {
            _adminRepository.DeleteBySkillId(id);
        }

        public Task<Industry> AddIndustry(Industry industry)
        {
            return _adminRepository.addIndustry(industry);
        }

        public Task<JobCategory> AddCategory(JobCategory category)
        {
            return _adminRepository.addCategory(category);
        }

        public Task<Location> AddLocation(Location location)
        {
            return _adminRepository.addLocation(location);
        }

    }
}
