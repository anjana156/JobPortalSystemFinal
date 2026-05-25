using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.Interfaces
{
    public interface IJobProviderRepository
    {
        public Task<List<JobPost>> GetJobs(Guid companyId);

        public Task<List<JobPost>> GetAllJobsByProvider(Guid companyId, Guid jobproviderId);

        public Task<List<JobApplication>> GetAllJobApplicants(Guid jobproviderId);

        public Task<List<JobProviderCompany>> GetCompany(Guid jobproviderId);
        public Task<Guid> Create(JobPost job);

        public Task<JobPost> UpdateAsync(JobPost Updatedjob, Guid id);

        public Task<JobPost> GetJobById(Guid jobId);

        public void DeleteJob(Guid id);

        Guid AddSignupRequest(SignUpRequest signUpRequest);

        Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobProviderSignupRequestId);

        void UpdateSignupRequest(SignUpRequest signUpRequest);
    }
}
