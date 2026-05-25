using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobPostS.DTO
{
    public class JobPostDto
    {
        public Guid Id { get; set; }

        public string JobTitle { get; set; } = null!;

        public string JobSummary { get; set; } = null!;

        public Guid LocationId { get; set; }

        public Guid CompanyId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid IndustryId { get; set; }

        public Guid PostedBy { get; set; }

        public DateTime PostedDate { get; set; }
    }
}
