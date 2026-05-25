using Domain.Application.Features.JobPostS.DTO;
using Domain.Application.Features.JobPostS.Interface;
using Domain.Application.Features.User.Interfaces;
using Domain.Application.Features.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.Application.Features.JobPostS.DTO.CreateJobDto;

namespace JobPortalSystem.API.Controllers.JobPostS
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "JOB_PROVIDER")]
    public class JobPostController : ControllerBase
    {
        private readonly IJobPostService _service;
        private readonly IUserService _userService;

        public JobPostController(
            IJobPostService service,
            IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(CreateJobPostDto dto)
        {
            Guid postedBy = new Guid(_userService.GetUserId());

            var result = await _service.CreateJob(dto, postedBy);

            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _service.GetAllJobs();

            return Ok(jobs);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetJobById(Guid id)
        {
            var job = await _service.GetJobById(id);

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(
    Guid id,
    [FromBody] UpdateJobDTO dto)
        {
            var result = await _service.UpdateJob(id, dto);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Job not found"
                });
            }

            return Ok(new
            {
                message = "Job updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var status = await _service.DeleteJob(id);

            if (status)
            {
                return Ok("Deleted Successfully");
            }

            return BadRequest();
        }
    }
}
