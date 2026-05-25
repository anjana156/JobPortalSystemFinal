using System.ComponentModel.DataAnnotations;

namespace JobPortalSystem.API.Controllers.Auth.RequestObjects
{
    public class SetPasswordRequest
    {
        [Required]
        public Guid SignupId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
