using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public partial class JobPost
    {
        public Guid Id { get; set; }

        public string JobTitle { get; set; } = null!;

        public string JobSummary { get; set; } = null!;
        [ForeignKey(nameof(Location))]
        public Guid Location_Id { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        [ForeignKey(nameof(JobCategory))]
        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(Industry))]
        public Guid IndustryId { get; set; }
        [ForeignKey(nameof(PostedByNavigation))]
        public Guid PostedBy { get; set; }

        public DateTime PostedDate { get; set; }

        public virtual Location Location { get; set; } = null!;
        public virtual Industry Industry { get; set; } = null!;
        public virtual JobProviderCompany Company { get; set; } = null!;

        public virtual ICollection<JobResponsibility> JobResponsibilities { get; set; } = new List<JobResponsibility>();

        public virtual CompanyUser PostedByNavigation { get; set; } = null!;
        public virtual JobCategory JobCategory { get; set; } = null!;

       
    }
}
