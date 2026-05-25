using Domain.Helpers;
using Domain.Models;

namespace Domain.Application.Features.Job.Interfaces
{
    public interface IJobRepository
    {
        // JOBS

        Task<List<JobPost>> GetJobs();

        Task<List<JobPost>> GetJobs(Guid userId);

        Task<List<JobPost>> GetJobsByCompany(Guid companyId);

        Task<JobPost?> GetJobsById(Guid companyId, Guid jobId);




        // SAVED JOBS

        Task<PagedList<SavedJob>> GetAllSavedJobsOfSeeker(
            Guid jobSeekerId,
            JobListParams param);

        Task<SavedJob> SaveJob(SavedJob savedJob);

        bool IsJobSaved(Guid jobId,Guid userId);

        Task<SavedJob?> GetSavedJobById(
            Guid jobSeekerId,
            Guid savedJobId);

        SavedJob? RemoveSavedJob(
            Guid jobSeekerId,
            Guid jobId);


        // APPLIED JOBS


        Task<PagedList<JobApplication>> GetAllAppliedJobs(
            Guid jobSeekerId,
            JobListParams param);

        bool ApplyJob(JobApplication applyJob);

        bool CancelAppliedJob(
            Guid jobSeekerId,
            Guid jobApplicationId);
    }
}