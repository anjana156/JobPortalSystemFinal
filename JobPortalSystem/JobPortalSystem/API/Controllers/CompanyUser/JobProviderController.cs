using AutoMapper;
using Domain.Application.Features.Job.DTO;
using Domain.Application.Features.JobProvider.DTO;
using Domain.Application.Features.JobProvider.Interfaces;
using Domain.Application.Features.Login.DTO;
using Domain.Application.Features.Login.Interfaces;
using Domain.Application.Features.SignUp.DTO;
using Domain.Models;
using JobPortalSystem.API.Controllers.CompanyUser.RequestObjects;
using JobPortalSystem.API.Controllers.JobSeeker.RequestObjects;
using JobPortalSystem.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalSystem.API.Controllers.CompanyUser
{
    [ApiController]
    [Authorize(Roles = "JOB_PROVIDER")]
    public class JobProviderController : BaseApiController<JobProviderController>
    {
        private readonly IJobProviderService _jobProviderService;
        private readonly IMapper _mapper;
        IJobProviderRepository _jobRepository;
        public ILoginRequestService _loginRequestService { get; set; }
        public JobProviderController(IJobProviderService jobProviderService, IMapper mapper, IJobProviderRepository jobProviderRepository, ILoginRequestService loginRequestService)
        {
            _jobProviderService = jobProviderService;
            _mapper = mapper;
            _jobRepository = jobProviderRepository;
            _loginRequestService = loginRequestService;
        }

        [HttpPost]
        [Route("job-provider/signup")]
        [AllowAnonymous]
        public async Task<ActionResult> createJobProviderSignupRequest(JobProviderSignupRequest data)
        {
            var jobSeekerSignupRequestDto = _mapper.Map<SignUpRequestDto>(data);
            _jobProviderService.CreateSignupRequest(jobSeekerSignupRequestDto);
            return Ok(data);
        }

        [HttpGet]
        [Route("job-provider/signup/{signupRequestId}/verify-email")]
        [AllowAnonymous]
        public async Task<ActionResult> VerifyJobProviderEmail(Guid signupRequestId)
        {
            var isVerified = await _jobProviderService.VerifyEmailAsync(signupRequestId);
            if (isVerified)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("job-provider/signup/{jobProviderSignupRequestId}/set-password")]
        [AllowAnonymous]
        public async Task<ActionResult> createJobProviderSignupRequest(Guid jobProviderSignupRequestId, [FromBody] string password)
        {
            await _jobProviderService.CreateJobProvider(jobProviderSignupRequestId, password);
            return Ok("Password Set Successfully");
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("job-provider/login")]
        //public async Task<ActionResult> Login(JobSeekerLoginRequest logdata)
        //{
        //    //var user = _mapper.Map<User>(userDto);
        //    var user = _loginRequestService.Login(logdata.Email, logdata.Password);

        //    if (user == null)
        //    {
        //        return BadRequest("Login Failed");
        //    }
        //    return Ok(user);
        //}




        [AllowAnonymous]
        [HttpGet]
        [Route("company/companyId")]
        public async Task<IActionResult> GetAllJobs(Guid companyId)
        {
            try
            {
                List<JobPost> jobposts = await _jobProviderService.GetJobs(companyId);
                return Ok(_mapper.Map<List<JobPostsDtos>>(jobposts));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("company/{companyId}/job-provider/{jobproviderId}/job")]
        public async Task<IActionResult> GetAllJobsByProvider(Guid companyId, Guid jobproviderId)
        {
            try
            {
                List<JobPost> jobposts = await _jobProviderService.GetAllJobsByProvider(companyId, jobproviderId);
                return Ok(_mapper.Map<List<JobPostsDtos>>(jobposts));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("company/{companyId}/job-provider/{jobproviderId}/job")]

        public async Task<IActionResult> PostJob(JobPostRequest request)
        {
            var job = _mapper.Map<JobPost>(request);
            Guid id = await _jobProviderService.PostJob(job);
            return Ok("The job id for the posted job is" + id);
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("company/{companyId}/job-provider/{jobproviderId}/job/{id}")]

        public async Task<IActionResult> UpdateJob(JobPostRequest request, Guid id)
        {
            try
            {

                /*jobpostDto.Id = id;*/
                var job = _mapper.Map<JobPost>(request);
                var jobUpdated = await _jobProviderService.Update(job, id);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpDelete]
        [Route("company/{companyId}/job-provider/{jobproviderId}/job/{id}")]

        public async Task<IActionResult> DeleteJob(Guid id)
        {
            try
            {
                _jobProviderService.DeleteJob(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("job-provider/{jobproviderId}/getJobApplicants")]
        public async Task<IActionResult> GetAllJobApplicants(Guid jobproviderId)
        {
            try
            {
                List<JobApplication> jobapplications = await _jobProviderService.GetAllJobApplicants(jobproviderId);
                return Ok(_mapper.Map<List<JobApplicationDto>>(jobapplications));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("job-provider/{jobproviderId}/getCompany")]
        public async Task<IActionResult> GetCompany(Guid jobproviderId)
        {
            try
            {
                List<JobProviderCompany> jobapplications = await _jobProviderService.GetCompany(jobproviderId);

                return Ok(_mapper.Map<List<Domain.Application.Features.JobProvider.DTO.JobProviderDto>>(jobapplications));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
