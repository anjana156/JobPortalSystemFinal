using Domain.Application.Features.SignUp.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.Interfaces
{
    public interface IJobProviderService
    {
        public Task<List<JobPost>> GetJobs(Guid companyId);

        public Task<List<JobPost>> GetAllJobsByProvider(Guid companyId, Guid jobproviderId);

        public Task<List<JobApplication>> GetAllJobApplicants(Guid jobproviderId);

        public Task<List<JobProviderCompany>> GetCompany(Guid jobproviderId);

        public Task<Guid> PostJob(JobPost job);

        public Task<JobPost> Update(JobPost job, Guid id);

        public Task<JobPost> GetJobById(Guid jobId);

        public void DeleteJob(Guid id);

        void CreateSignupRequest(SignUpRequestDto data);

        Task<bool> VerifyEmailAsync(Guid jobProviderSignupRequestId);

        Task CreateJobProvider(Guid jobSeekerSignupRequestId, string password);
    }
}
