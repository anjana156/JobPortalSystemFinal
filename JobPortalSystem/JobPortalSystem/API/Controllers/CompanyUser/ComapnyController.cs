using AutoMapper;
using Domain.Application.Features.JobProvider.DTO;
using Domain.Application.Features.JobProvider.Interfaces;
using Domain.Helpers;
using Domain.Application.Features.Authuser.DTO;
using Domain.Application.Features.Authuser.Interfaces;
using JobPortalSystem.API.Controllers.CompanyUser.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalSystem.API.Controllers.CompanyUser
{

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CompanyController : ControllerBase
    {
        public CompanyController(IMapper _mapper, ICompanyService _companyService, IAuthUserService _authUserService)
        {
            mapper = _mapper;
            companyService = _companyService;
            authUserService = _authUserService;
        }

        public IMapper mapper { get; set; }
        public ICompanyService companyService { get; set; }
        public IAuthUserService authUserService { get; set; }

        [HttpPost]
        [Route("job-provider/{jobproviderId}/company")]

        public async Task<ActionResult> AddCompany(Guid jobproviderId, AddCompanyRequestobject data)
        {
           //var UserId = authUserService.GetUserId();
            var companyRegistrationDtos = mapper.Map<CompanyRegistrationDtos>(data);

           // var company = await companyService.AddCompany(companyRegistrationDtos, new Guid(UserId));
           // return Ok(company);


            var userId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var company = await companyService.AddCompany(companyRegistrationDtos, userId);

            return Ok(company);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("job-provider/company/{companyId}")]
        public async Task<ActionResult> getCompany(Guid companyId)
        {
            var company = companyService.GetCompany(companyId);
            if (company == null)
            {
                return BadRequest("Company Not found");

            }
            else
            {
                return Ok(company);
            }


        }
        [AllowAnonymous]
        [HttpPut]
        [Route("job-provider/company/{companyId}")]
        public async Task<ActionResult> UpdateCompany(Guid companyId, CompanyupdateRequest comapny)
        {
            if (companyId == null)
            {
                return BadRequest("Id is Required");
            }
            comapny.Id = companyId;
            var companyUpdateDtos = mapper.Map<CompanyUpdateDtos>(comapny);
            var updatedCompany = await companyService.UpdateAsync(companyUpdateDtos);
            //CompanyupdateRequest companyupdateRequest = mapper.Map<CompanyupdateRequest>(updatedCompany);
            if (updatedCompany == null)
            {
                return BadRequest("Company Not found");

            }
            else
            {
                return Ok(updatedCompany);
            }

        }

        //Add-Company-Member

        [AllowAnonymous]
        [HttpPost]
        [Route("job-provider/company/{companyId}/addcompanymember")]
        public async Task<ActionResult> AddCompanyMember(companyUserRequest request, Guid companyId)
        {
            try
            {
                var companyMemberDtos = mapper.Map<CompanyMemberDtos>(request);
                var member = await companyService.addMember(companyMemberDtos, companyId);
                return Ok(member);
            }
            catch (Exception exe)
            {
                return BadRequest(exe.Message);
            }
        }

        //

        [AllowAnonymous]
        [HttpGet]
        [Route("job-provider/company/{companyId}/listcompanymember")]
        public async Task<ActionResult> ListCompanyMember(Guid companyId, [FromQuery] CompanyMemberListParam param)

        {

            if (companyId == null)
            {
                return BadRequest("Id is Required");
            }

            var CompanyMembers = await companyService.memberListing(companyId, param);

            PagedList<CompanyMemberListDtos> companyMemberList = mapper.Map<PagedList<CompanyMemberListDtos>>(CompanyMembers);
            if (CompanyMembers == null)
            {
                return BadRequest("No Company Members");

            }
            else
            {
                return Ok(CompanyMembers);
            }

        }
        [AllowAnonymous]
        [HttpDelete]
        [Route("job-provider/company/{companyMemberId}/RemoveCompanyMember")]
        public IActionResult memberDelete(Guid companyMemberId)
        {
            var result = companyService.memberDeleteById(companyMemberId);
            if (result == true)
            {
                return Ok("Success fully remove the companyMember");

            }
            else
            {
                return BadRequest();
            }
        }





    }
}
