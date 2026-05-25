using AutoMapper;
using Domain.Application.Features.JobProvider.DTO;
using Domain.Application.Features.JobProvider.Interfaces;
using Domain.Enums;
using Domain.Helpers;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.Repositories
{
    public class InterviewRepository : IInterviewRepository
    {
        JobPortalDbContext context;
        public IMapper mapper { get; set; }

        public InterviewRepository(JobPortalDbContext context, IMapper _mapper)
        {
            this.context = context;
            mapper = _mapper;
        }

        public Interview shduleInterview(InterviewsheduleDtos interview, CompanyUser user)

        {
            try
            {
                JobApplication applicaction = context.JobApplications.Where(a => a.Id == interview.ApplicationId).Include(e => e.JobPost).FirstOrDefault();
                var interviewtoshedule = mapper.Map<Interview>(interview);
                interviewtoshedule.JobId = applicaction.JobPost_id;
                interviewtoshedule.ApplicationId = applicaction.Id;
                interviewtoshedule.Status = JobInterviewStatus.SCHEDULED;
                interviewtoshedule.SheduledBy = user.Id;
                interviewtoshedule.interviewee = applicaction.Applicant;
                interviewtoshedule.CompanyId = (Guid)user.Company;


                context.Interviews.AddAsync(interviewtoshedule);
                context.SaveChanges();
                return interviewtoshedule;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public async Task<PagedList<Interview>> sheduledInterviewList(Guid companyid, InterviewParams param)
        {
            var query = context.Interviews
               .OrderByDescending(c => c.Date).Where(e => e.CompanyId == companyid).Include(e => e.Job).Include(e => e.Jobseeker).Include(e => e.Application).Include(e => e.Company).Include(e => e.CompanyUser)
               .AsQueryable();
            return await PagedList<Interview>.CreateAsync(query,
                    param.PageNumber, param.PageSize);


        }
        public bool removeInterview(Guid id)
        {
            Interview interview = context.Interviews.FirstOrDefault(e => e.Id == id);
            if (interview != null)
            {
                context.Interviews.Remove(interview);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
