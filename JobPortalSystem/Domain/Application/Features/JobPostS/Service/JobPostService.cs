using AutoMapper;
using Domain.Application.Features.JobPostS.DTO;
using Domain.Application.Features.JobPostS.Interface;
using Domain.Application.Features.JobPostS.DTO;
using Domain.Application.Features.JobPostS.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Application.Features.JobPostS.DTO.CreateJobDto;


namespace Domain.Application.Features.JobPostS.Service
{
    public class JobPostService : IJobPostService
    {
        private readonly IJobPostRepository _repository;
        private readonly IMapper _mapper;

    public JobPostService(IJobPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<JobPostDto> CreateJob(CreateJobPostDto dto, Guid postedBy)
    {
        JobPost jobPost = new JobPost
        {
            Id = Guid.NewGuid(),
            JobTitle = dto.JobTitle,
            JobSummary = dto.JobSummary,
            Location_Id = dto.LocationId,
            CompanyId = dto.CompanyId,
            CategoryId = dto.CategoryId,
            IndustryId = dto.IndustryId,
            PostedBy = postedBy,
            PostedDate = DateTime.Now
        };

        var result = await _repository.AddJobPost(jobPost);

        return _mapper.Map<JobPostDto>(result);
    }

    public async Task<List<JobPostDto>> GetAllJobs()
    {
        var jobs = await _repository.GetAllJobs();

        return _mapper.Map<List<JobPostDto>>(jobs);
    }

    public async Task<JobPostDto?> GetJobById(Guid id)
    {
        var job = await _repository.GetJobById(id);

        if (job == null)
        {
            return null;
        }

        return _mapper.Map<JobPostDto>(job);
    }
        public async Task<bool> UpdateJob(Guid id, UpdateJobDTO dto)
        {
            // Find job from database
            var job = await _repository.GetById(id);

            // Check job exists
            if (job == null)
                return false;

            // Update fields
            job.JobTitle = dto.JobTitle;

            job.JobSummary = dto.JobSummary;

            //job.Location = dto.Location;

            //job.Salary = dto.Salary;

            //job.ExpiryDate = dto.ExpiryDate;

            // Save changes
            await _repository.Update(job);

            return true;
        }

        public async Task<bool> DeleteJob(Guid id)
    {
        return await _repository.DeleteJob(id);
    }

}
}
