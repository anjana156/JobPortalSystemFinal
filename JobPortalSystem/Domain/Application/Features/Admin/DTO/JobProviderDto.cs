using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.Admin.DTO
{
    public class JobProviderDto
    {
        public Guid Id { get; set; }
        public string LegalName { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public Guid IndustryId { get; set; }

        public string Email { get; set; } = null!;

        public long Phone { get; set; }

        public string Address { get; set; } = null!;

        public string Website { get; set; } = null!;

        public Guid Location { get; set; }
    }
}
