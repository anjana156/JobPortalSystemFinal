using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.DTO
{
    public class SheduledInterviewDto
    {
        public Guid Id { get; set; }
        public string JobJobTitle { get; set; }
        public string JobseekerUsername { get; set; }

        public Guid ApplicationId { get; set; }

        public DateTime? Date { get; set; }


        public JobInterviewStatus Status { get; set; }
        public string CompanyUserUserName { get; set; }

        /*      public virtual CompanyUser? CompanyUser { get; set; }

              public virtual JobPost? Job { get; set; }

              public virtual JobSeeker? Jobseeker { get; set; }

              [ForeignKey(nameof(Company))]
              public Guid CompanyId { get; set; }
              public virtual JobProviderCompany Company { get; set; }
      */

    }
}
