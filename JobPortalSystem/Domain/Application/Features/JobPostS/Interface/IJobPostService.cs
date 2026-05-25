using Domain.Application.Features.JobPostS.DTO;
using Domain.Application.Features.JobPostS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Application.Features.JobPostS.DTO.CreateJobDto;

namespace Domain.Application.Features.JobPostS.Interface
{
    public interface IJobPostService
    {
        Task<JobPostDto> CreateJob(CreateJobPostDto dto, Guid postedBy);

        Task<List<JobPostDto>> GetAllJobs();

        Task<JobPostDto?> GetJobById(Guid id);

        Task<bool> DeleteJob(Guid id);
        Task<bool> UpdateJob(Guid id, UpdateJobDTO dto);
    }
}
