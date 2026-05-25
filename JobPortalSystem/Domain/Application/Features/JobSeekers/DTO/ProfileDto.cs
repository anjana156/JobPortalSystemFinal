using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobSeekers.DTO
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public Guid JobSeekerId { get; set; }

        public string? ProfileName { get; set; }

        public string? ProfileSummary { get; set; }

        public Guid? ResumeId { get; set; }
       
    }
}
