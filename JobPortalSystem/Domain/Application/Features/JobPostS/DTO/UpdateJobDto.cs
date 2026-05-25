using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobPostS.DTO
{
    public class UpdateJobDTO
    {
        public string JobTitle { get; set; } = string.Empty;

        public string JobSummary { get; set; } = string.Empty;

        //public string Location { get; set; } = string.Empty;

        //public decimal Salary { get; set; }

        //public DateTime ExpiryDate { get; set; }
    }
}
