using AutoMapper;
using Domain.Application.Features.Job.DTO;
using Domain.Application.Features.Job.Interfaces;
using Domain.Helpers;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.Job.Repositories
{
    public class JobRepository : IJobRepository
    {

        JobPortalDbContext _context;
        IMapper _mapper;
        static List<JobPost> joblist;

        public JobRepository(JobPortalDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET ALL JOBS
        public async Task<List<JobPost>> GetJobs()
        {

          return await _context.JobPosts.ToListAsync();
        }

        public async Task<List<JobPost>> GetJobs(Guid userId)
        {
            return await _context.JobPosts.ToListAsync();
        }

        public async Task<List<JobPost>> GetJobsByCompany(Guid companyId)
        {
            /*   return await _context.JobPosts.Include(j => j.Company== companyId).ToListAsync();*/
            return await _context.JobPosts.Where(e => e.CompanyId == companyId).ToListAsync();
        }


        //public async Task<List<JobPost>> GetJobsById(Guid companyId, Guid jobId)
        //{
        //    return await _context.JobPosts.Where(e => e.CompanyId == companyId && e.Id == jobId).ToListAsync();
        //}




        public async Task<JobPost?> GetJobsById(Guid companyId, Guid jobId)
        {
            return await _context.JobPosts
                .FirstOrDefaultAsync(j => j.CompanyId == companyId && j.Id == jobId);
        }




        // SAVED JOBS



        public async Task<PagedList<SavedJob>> GetAllSavedJobsOfSeeker(Guid jobseekerId, JobListParams param)
        {

            var query = _context.SavedJobs
              .OrderByDescending(c => c.DateSaved).Where(e => e.SavedBy == jobseekerId).Include(e => e.JobPost).AsQueryable();
            return await PagedList<SavedJob>.CreateAsync(query,
            param.PageNumber, param.PageSize);
        }



        public async Task<SavedJob> SaveJob(SavedJob savedJob)
        {
            await _context.SavedJobs.AddAsync(savedJob);
            await _context.SaveChangesAsync();
            return savedJob;
        }

        public bool IsJobSaved(Guid jobId, Guid userId)
        {
            return _context.SavedJobs.Any(e => e.Job == jobId && e.SavedBy == userId);
        }

        //public SavedJob GetsavedJobById(Guid jobseekerId, Guid SavedJobId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<SavedJob?> GetSavedJobById(Guid jobseekerId, Guid savedJobId)
        {
            return await _context.SavedJobs.Include(e => e.JobPost)
                .FirstOrDefaultAsync(s => s.SavedBy == jobseekerId && s.Id == savedJobId);
        }


        public SavedJob RemoveSavedJob(Guid jobseekerId, Guid jobid)
        {
            var savedjob = _context.SavedJobs.FirstOrDefault(e => e.SavedBy == jobseekerId && e.Id == jobid);
            if(savedjob != null)
            {
                _context.SavedJobs.Remove(savedjob);
                _context.SaveChanges();
            }
            return savedjob;
        }

        // APPLIED JOBS
        public async Task<PagedList<JobApplication>> GetAllAppliedJobs(Guid jobseekerId, JobListParams param)
        {
            try
            {
                var query = _context.JobApplications.AsQueryable().Where(e => e.Applicant == jobseekerId).Include(e => e.JobPost).Include(e => e.JobPost.Company);


                return await PagedList<JobApplication>.CreateAsync(query,
                    param.PageNumber, param.PageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ApplyJob(JobApplication applyJob)
        {
            //applyjob.status = Enums.Status.PENDING;
            _context.JobApplications.Add(applyJob);
            _context.SaveChanges();
            return true;

        }
        public bool CancelAppliedJob(Guid jobseekerId, Guid JobApplicationId)
        {
            try
            {
                var AppliedJob = _context.JobApplications.Where(e => e.Id == JobApplicationId).FirstOrDefault();
                if (AppliedJob != null)
                {
                    _context.JobApplications.Remove(AppliedJob);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        
    }

}