using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobPostS.DTO
{
    public class CreateJobDto
    {
        public class CreateJobPostDto
        {
            public string JobTitle { get; set; } = null!;

            public string JobSummary { get; set; } = null!;

            public Guid LocationId { get; set; }

            public Guid CompanyId { get; set; }

            public Guid CategoryId { get; set; }

            public Guid IndustryId { get; set; }
        }

    }
}
