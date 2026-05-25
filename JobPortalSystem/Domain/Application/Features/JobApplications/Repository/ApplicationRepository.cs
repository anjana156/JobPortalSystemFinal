using Domain.Application.Features.JobApplications.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobApplications.Repository
{
    public  class ApplicationRepository : IApplicationRepository
    {
        private readonly JobPortalDbContext _context;
        public ApplicationRepository(JobPortalDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<JobApplication>> GetApplicationByJobId(Guid jobId)
        {
            return await _context.JobApplications
                .Include(x => x.Seeker)
                .Include(x => x.Resume)
                .Include(x => x.JobPost)
                .Where(x => x.JobPost_id == jobId)
                .ToListAsync();
        }
        public async Task<JobApplication?> GetById(Guid id)
        {
            return await _context.JobApplications
               .Include(x => x.Seeker)
               .Include(x => x.Resume)
               .Include(x => x.JobPost)
               .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Update(JobApplication application)
        {
            _context.JobApplications.Update(application);

            await _context.SaveChangesAsync();
        }
    }
}
