using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobApplications.DTO
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public Guid JobPost_id { get; set; }
        public  Guid Applicant {  get; set; }
        public Guid Resume_id { get; set; }
        public string CoverLetter { get; set; }
        public DateTime Datesubmitted { get; set; }
        public string Status { get; set; }
    }
}
