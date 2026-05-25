using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobApplications.Interface
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetApplicationByJobId(Guid jobId);
        Task<JobApplication?> GetById(Guid id);
        Task Update(JobApplication application);
    }
}
