using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.Login.DTO
{
    public class LoginResponseDto
    {
        // Response
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Phone { get; set; }
        public Domain.Enums.Role Role { get; set; }

        // JWT
        public string? Token { get; set; }
    }
}
