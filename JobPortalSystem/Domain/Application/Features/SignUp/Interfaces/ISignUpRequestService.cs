
using Domain.Application.Features.SignUp.DTO;
using Domain.Models;

namespace Domain.Application.Features.SignUp.In
{
    public interface ISignUpRequestService
    {
        Task<Guid> CreateSignupRequest(SignUpRequestDto data);
        Task<bool> VerifyEmailAsync(Guid signupId);
        Task CreateUserAccount(Guid signupId, string password); 
      

        

    }
}
