using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("AuthUser")]
    public partial class AuthUser : SystemUser
    {

        public string? Password { get; set; }
        
        public string? ConnectionId { get; set; }
        public bool? OnlineStatus { get; set; } = false;

    }
}
