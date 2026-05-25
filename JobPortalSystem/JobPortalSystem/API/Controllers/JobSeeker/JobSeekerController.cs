using AutoMapper;
using Domain.Application.Features.Job.Interfaces;
using Domain.Application.Features.Job.Services;
using Domain.Application.Features.JobSeeker.Interfaces;
using Domain.Application.Features.JobSeekers.DTO;
using Domain.Helpers;
using Domain.Models;
using JobPortalSystem.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace JobPortalSystem.API.Controllers.JobSeeker
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "JOB_SEEKER")]

    public class JobSeekerController : BaseApiController<JobSeekerController>
    {
        private readonly IJobSeekerService _jobSeekerService;
        private readonly IJobServices _jobService;
     

        public IMapper _mapper { get; set; }
        public JobSeekerController(IJobSeekerService jobSeekerService, IMapper mapper, IJobServices jobService)
        {
            _jobSeekerService = jobSeekerService;
            _mapper = mapper;
            _jobService = jobService;

        }


        // GET LOGGED-IN USER ID

        private Guid GetUserId()
        {
            return Guid.Parse(
                User.FindFirstValue(ClaimTypes.Sid));
        }



        // GET PROFILE

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();

            var profile =
                await _jobSeekerService
                    .GetcompleteProfile(userId);

            return Ok(profile);
        }



        // SAVE JOB

        [HttpPost("SaveJob/{jobId}")]
        public async Task<IActionResult> SaveJob(Guid jobId)
        {
            SavedJob savedJob = new SavedJob();

            savedJob.Job = jobId;

            savedJob.SavedBy = GetUserId();

            savedJob.DateSaved = DateTime.UtcNow;

            await _jobService.SaveJob(savedJob);

            return Ok("Job Saved");
        }



        // GET SAVED JOBS

        [HttpGet("SavedJobs")]
        public async Task<IActionResult> GetSavedJobs(
            [FromQuery] JobListParams param)
        {
            var jobs =
                await _jobService.GetAllSavedJobsOfSeeker(
                    GetUserId(),
                    param);

            return Ok(jobs);
        }



        // APPLY JOB

        [HttpPost("ApplyJob/{jobId}")]
        public IActionResult ApplyJob(Guid jobId, Guid resumeId)
        {
            JobApplication application = new JobApplication();

            application.JobPost_id = jobId;

            application.Applicant = GetUserId();

            application.Resume_id= resumeId;

            application.Datesubmitted = DateTime.UtcNow;

            application.Status = Domain.Enums.Status.PENDING;

            var result =
                _jobService.ApplyJob(application);

            return Ok(result);
        }



        // GET APPLIED JOBS

        [HttpGet("AppliedJobs")]
        public async Task<IActionResult> GetAppliedJobs(
            [FromQuery] JobListParams param)
        {
            var jobs =
                await _jobService.GetAllAppliedJobs(
                    GetUserId(),
                    param);

            return Ok(jobs);
        }

    }
}
