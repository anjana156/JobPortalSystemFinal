using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.DTO
{
    public class InterviewsheduleDtos
    {
        public Guid? ApplicationId { get; set; }
        public DateTime? Date { get; set; }
        public JobPost Job { get; set; }
        public JobProviderCompany Company { get; set; }
        public Models.JobSeeker Jobseeker { get; set; }
        public virtual CompanyUser? CompanyUser { get; set; }


    }
}
