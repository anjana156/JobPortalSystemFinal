using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.Admin.DTO
{
    public class JobSeekerDto
    {
        public string? UserName { get; set; }

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int Role { get; set; }
    }
}
