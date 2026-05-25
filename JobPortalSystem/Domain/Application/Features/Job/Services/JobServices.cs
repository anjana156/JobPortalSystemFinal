using AutoMapper;
using Domain.Application.Features.Job.DTO;
using Domain.Application.Features.Job.Interfaces;
using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.Job.Services
{
    public class JobServices : IJobServices
    {
        private IJobRepository _jobrepository;
        private IMapper _mapper;

        public JobServices(IJobRepository jobrepository, IMapper mapper)
        {
            _jobrepository = jobrepository;
            _mapper = mapper;
        }

        // GET ALL JOBS
        public async Task<List<JobPostsDtos>> GetJobs()
        {
            var notApplied = await _jobrepository.GetJobs();
            var dtoList = _mapper.Map<List<JobPostsDtos>>(notApplied);
            return dtoList;


        }
        public async Task<List<JobPostsDtos>> GetJobs(Guid userId)
        {
            var notApplied = await _jobrepository.GetJobs(userId);
            var dtoList = _mapper.Map<List<JobPost>, List<JobPostsDtos>>(notApplied);

            foreach (var job in dtoList)
            {
                job.Saved = _jobrepository.IsJobSaved(job.Id, userId);
            }

            return dtoList;
        }
       

        public async Task<List<JobPost>> GetJobsByCompany(Guid companyId)
        {
            return await _jobrepository.GetJobsByCompany(companyId);
        }

        //public async Task<List<JobPost>> GetJobsById(Guid companyId, Guid jobId)
        //{
        //    return await _jobrepository.GetJobsById(companyId, jobId);
        //}

        public async Task<JobPost?> GetJobsById(Guid companyId, Guid jobId)
        {
            return await _jobrepository.GetJobsById(companyId, jobId);
        }


        //SAVED JOBS

        public async Task<PagedList<SavedJob>> GetAllSavedJobsOfSeeker(Guid jobseekerId, JobListParams param)
        {
            var savedJobs = await _jobrepository.GetAllSavedJobsOfSeeker(jobseekerId, param);
            //var savedjobsDto = _mapper.Map<PagedList<SavedJob>>(savedJobs);
            return savedJobs;
        }

        public async Task<SavedJob> SaveJob(SavedJob savedJob)
        {
            return await _jobrepository.SaveJob(savedJob);
        }

        public async Task<SavedJobsDtos?> GetSavedJobById(Guid jobseekerId, Guid savedJobId)
        {
            var savedJob = await _jobrepository.GetSavedJobById(jobseekerId, savedJobId);
            if(savedJob == null)
            {
                return null;
            }
                return _mapper.Map<SavedJobsDtos>(savedJob);
            
        }

        public SavedJob? RemoveSavedJob(Guid seekerId, Guid jobid)
        {

            return _jobrepository.RemoveSavedJob(seekerId, jobid);
        }



        // APPLIED JOBS
        public async Task<PagedList<AppliedJobsDtos>> GetAllAppliedJobs(Guid jobseekerId, JobListParams param)
        {
            var appliedjobs = await _jobrepository.GetAllAppliedJobs(jobseekerId, param);

            var appliedjobsDto = _mapper.Map<PagedList<AppliedJobsDtos>>(appliedjobs);
            return appliedjobsDto;
        }
        

        public bool ApplyJob(JobApplication applyJob)

        {

            return _jobrepository.ApplyJob(applyJob);
        }
       
        public bool CancelAppliedJob(Guid jobseekerId, Guid JobApplicationId)
        {
            return _jobrepository.CancelAppliedJob(jobseekerId, JobApplicationId);
        }
       
       

    }

}
