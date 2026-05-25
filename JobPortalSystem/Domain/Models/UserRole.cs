using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    [Table("UserRoles")]

    public partial class UserRole
    {
        

        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }

}
