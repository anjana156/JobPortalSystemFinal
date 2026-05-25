using Domain.Application.Features.JobProvider.DTO;
using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.Interfaces
{
    public interface IInterviewService
    {
        Interview sheduleinterview(InterviewsheduleDtos interview, CompanyUser userId);
        Task<PagedList<Interview>> sheduledInterviewList(Guid companyid, InterviewParams param);
        bool removeInterview(Guid id);

    }
}
