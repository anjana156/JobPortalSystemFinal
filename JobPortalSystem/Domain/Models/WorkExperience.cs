using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public partial class WorkExperience
    {
        public Guid Id { get; set; }

        public Guid JobSeekerProfileId { get; set; }

        public string JobTitle { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public DateTime ServiceStart { get; set; }

        public DateTime ServiceEnd { get; set; }

        public virtual JobSeekerProfile JobSeekerProfile { get; set; } = null!;
    }

}
