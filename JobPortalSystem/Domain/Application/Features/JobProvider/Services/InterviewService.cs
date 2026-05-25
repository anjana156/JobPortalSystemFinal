using Domain.Application.Features.JobProvider.DTO;
using Domain.Application.Features.JobProvider.Interfaces;
using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.Services
{
    public class InterviewService : IInterviewService
    {
        public InterviewService(IInterviewRepository _interviewRepository)
        {
            interviewRepository = _interviewRepository;
        }

        public IInterviewRepository interviewRepository { get; set; }
        public Interview sheduleinterview(InterviewsheduleDtos interview, CompanyUser userId)
        {
            return interviewRepository.shduleInterview(interview, userId);
        }
        public async Task<PagedList<Interview>> sheduledInterviewList(Guid companyid, InterviewParams param)
        {
            return await interviewRepository.sheduledInterviewList(companyid, param);
        }
        public bool removeInterview(Guid id)
        {
            return interviewRepository.removeInterview(id);
        }

    }
}
