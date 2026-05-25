using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;


namespace Domain.Application.Features.JobPostS.Interface
{
    public interface IJobPostRepository
    {
        Task<JobPost> AddJobPost(JobPost jobPost);

        Task<List<JobPost>> GetAllJobs();

        Task<JobPost?> GetJobById(Guid id);

        Task<bool> DeleteJob(Guid id);
        Task<JobPost?> GetById(Guid id);

        Task Update(JobPost job);
    }
}
