using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public partial class JobProviderCompany
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string LegalName { get; set; } = null!;

        public string Summary { get; set; } = null!;
        [ForeignKey(nameof(Industry))]
        public Guid IndustryId { get; set; }

        public string Email { get; set; } = null!;

        public long Phone { get; set; }

        public string Address { get; set; } = null!;

        public string Website { get; set; } = null!;
        [ForeignKey(nameof(LocationNavigation))]
        public Guid Location { get; set; }
        public virtual Industry Industry { get; set; }

        public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();

        public virtual Location LocationNavigation { get; set; }

    }

}
