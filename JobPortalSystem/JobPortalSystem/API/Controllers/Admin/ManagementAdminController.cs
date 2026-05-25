using AutoMapper;
using Domain.Application.Features.Admin.DTO;
using Domain.Application.Features.Admin.Interfaces;
using Domain.Application.Features.Profile.DTO;
using Domain.Models;
using Domain.Application.Features.Admin.Services;
using Domain.Application.Features.User.Interfaces;
using JobPortalSystem.API.Controllers.Admin.RequestObjects;
using JobPortalSystem.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Application.Features.JobSeekers.DTO;

namespace JobPortalSystem.API.Controllers.Admin
{


    [ApiController]

    [Route("api/admin")]

    [Authorize(Roles = "ADMIN")]
    public class ManagementAdminController : BaseApiController<ManagementAdminController>
    {
        private readonly IAdminServices _adminService;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepository;
       

        public ManagementAdminController(IMapper mapper, IAdminServices adminService, IAdminRepository adminRepository)
        {
            _mapper = mapper;
            _adminService = adminService;
            _adminRepository = adminRepository;
           
        }


        [HttpGet]
        [Route("GetJobSeekers")]
        public async Task<IActionResult> GetJobSeekers()
        {

            try
            {
                var jobSeekers = await _adminService.GetJobSeekers();
                return Ok(_mapper.Map<List<JobSeekerDto>>(jobSeekers));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpGet]
        [Route("GetCompanyUsers")]
        public async Task<IActionResult> GetCompanyUsers()
        {

            try
            {
                var companyUsers = await _adminService.GetCompanyUsers();
                return Ok(_mapper.Map<List<CompanyUsersDto>>(companyUsers));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("RemoveCompanyUsers/{id}")]
        public IActionResult Remove(Guid id)
        {
            try
            {
                _adminService.DeleteById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetCompanies")]
        public async Task<IActionResult> GetCompanies()
        {

            try
            {
                var jobProviders = await _adminService.GetCompanies();
                return Ok(_mapper.Map<List<JobProviderDto>>(jobProviders));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        //New-Code

        [HttpGet]
        [Route("SearchCompanies")]
        public async Task<IActionResult> SearchCompanies(string name)
        {

            try
            {

                var companies = await _adminService.SearchCompanies(name);
                return Ok(_mapper.Map<List<JobProviderDto>>(companies));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("RemoveCompanies/{id}")]
        public IActionResult RemoveCompanies(Guid id)
        {
            try
            {
                _adminService.DeleteCompaniesById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        //New-Code Ends

        [HttpGet]
        [Route("AllJobs")]
        public async Task<IActionResult> alljobs()
        {

            try
            {
                var jobs = await _adminService.GetJobs();
                return Ok(_mapper.Map<List<Joblist>>(jobs));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("JobsByName")]
        public async Task<IActionResult> getalljobs(string Title)
        {

            try
            {
                var jobs = await _adminService.GetJobs(Title);
                return Ok(_mapper.Map<List<Joblist>>(jobs));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
                

        [HttpGet]
        [Route("GetCompanyCount")]
        public IActionResult GetCompanyCount()
        {
            try
            {
                var count = _adminService.GetCompanyCount();
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetJobProviderCount")]
        public IActionResult GetJobProviderCount()
        {
            try
            {
                var count = _adminService.GetJobProviderCount();
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetJobCount")]
        public IActionResult GetJobCount()
        {
            try
            {
                var count = _adminService.GetJobCount();
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPost("AddSkill")]
        public async Task<IActionResult> AddSkill(SkillRequest skill)
        {
            var skillDto = _mapper.Map<SkillDto>(skill);

            var result = await _adminService.AddSkill(skillDto);

            return Ok(result); // returns id, name, description
        }


        

        [HttpGet("GetSkills")]
        public async Task<IActionResult> GetSkills()
        {

            try
            {
                var skills = await _adminService.GetSkills();
                return Ok(_mapper.Map<List<SkillDto>>(skills));

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpDelete("RemoveSkill/{id}")]
        public IActionResult RemoveSkill(Guid id)
        {
            // Call the service

            try
            {
                _adminService.DeleteBySkillId(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

       


        [HttpPost("AddIndustry")]
        public async Task<IActionResult> AddIndustry(IndustryRequest Industry)
        {
            var industry = _mapper.Map<Industry>(Industry);
            var result = await _adminService.AddIndustry(industry);

            return Ok(result);
        }


        [HttpGet("GetIndustries")]
        public async Task<IActionResult> GetIndustries()
        {

            try
            {
                var industries = await _adminService.GetIndustries();
                return Ok(_mapper.Map<List<Industry>>(industries));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }




        [HttpDelete]
        [Route("RemoveIndustry/{id}")]
        public IActionResult RemoveIndustry(Guid id)
        {
            try
            {
                _adminService.DeleteByIndustryId(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryRequest category)
        {
            var Category = _mapper.Map<JobCategory>(category);
            var result = await _adminService.AddCategory(Category);

            return Ok(result);
        }




        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {

            try
            {
                var categories = await _adminService.GetCategories();
                return Ok(_mapper.Map<List<JobCategory>>(categories));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("RemoveCategory/{id}")]
        public IActionResult RemoveCategory(Guid id)
        {
            try
            {
                _adminService.DeleteByCategoryId(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPost("AddLocation")]
        public async Task<IActionResult> AddLocation(LocationRequest location)
        {
            var Location = _mapper.Map<Location>(location);
            var result = await _adminService.AddLocation(Location);

            return Ok(result);
        }

        [HttpGet("GetLocations")]
        public async Task<IActionResult> GetLocations()
        {

            try
            {
                var locations = await _adminService.GetLocations();
                return Ok(_mapper.Map<List<LocationDto>>(locations));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpDelete]
        [Route("RemoveLocations/{id}")]
        public IActionResult RemoveLocation(Guid id)
        {
            try
            {
                _adminService.DeleteByLocationId(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
