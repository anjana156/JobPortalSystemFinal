using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.DTO
{
    public class GetCompanyDetailsDto
    {
        public string LegalName { get; set; } = null!;

        public string Summary { get; set; } = null!;


        public string Email { get; set; } = null!;

        public long Phone { get; set; }

        public string Address { get; set; } = null!;

        public string Website { get; set; } = null!;




    }
}
