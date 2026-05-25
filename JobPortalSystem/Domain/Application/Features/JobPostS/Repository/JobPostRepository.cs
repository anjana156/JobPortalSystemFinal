using Domain.Application.Features.JobPostS.Interface;
using Domain.Application.Features.JobPostS.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobPostS.Repository
{

    public class JobPostRepository : IJobPostRepository
    {
        private readonly JobPortalDbContext _context;
        public JobPostRepository(JobPortalDbContext context)
        {
            _context = context;
        }

        public async Task<JobPost> AddJobPost(JobPost jobPost)
        {
            await _context.JobPosts.AddAsync(jobPost);
            await _context.SaveChangesAsync();

            return jobPost;
        }

        public async Task<List<JobPost>> GetAllJobs()
        {
            return await _context.JobPosts
                .Include(j => j.Company)
                .Include(j => j.Location)
                .Include(j => j.JobCategory)
                .Include(j => j.Industry)
                .ToListAsync();
        }

        public async Task<JobPost?> GetJobById(Guid id)
        {
            return await _context.JobPosts
                .Include(j => j.Company)
                .Include(j => j.Location)
                .Include(j => j.JobCategory)
                .Include(j => j.Industry)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<bool> DeleteJob(Guid id)
        {
            var job = await _context.JobPosts
                .FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return false;
            }

            _context.JobPosts.Remove(job);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<JobPost?> GetById(Guid id)
        {
            return await _context.JobPosts
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(JobPost job)
        {
            _context.JobPosts.Update(job);

            await _context.SaveChangesAsync();
        }
    }
}
