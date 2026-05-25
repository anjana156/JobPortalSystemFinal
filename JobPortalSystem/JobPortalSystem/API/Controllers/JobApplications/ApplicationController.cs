using Domain.Application.Features.JobApplications.DTO;
using Domain.Application.Features.JobApplications.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalSystem.API.Controllers.JobApplications
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _service;
        public ApplicationController(IApplicationService service)
        {
            _service = service;
        }

        [HttpGet("job/jobId")]
        public async Task<IActionResult> GetApplicationByJob(Guid jobId)
        {
            var result = await _service.GetApplicationByJobId(jobId);
            return Ok(result);

        }
        [HttpGet("{id}")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
    Guid id,
    [FromBody] UpdateStatusDTO dto)
        {
            var result = await _service.UpdateStatus(id, dto);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Invalid application or status"
                });
            }

            return Ok(new
            {
                message = "Application status updated successfully"
            });
        }
    }
    }

