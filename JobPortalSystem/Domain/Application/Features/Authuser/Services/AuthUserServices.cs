using Domain.Application.Features.Authuser.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Domain.Application.Features.Authuser.Services
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthUserRepository _userRepository;

        public AuthUserService(IHttpContextAccessor httpContextAccessor, IAuthUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public string GetUserId()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value.ToString();
            }
            return result;
        }
        public CompanyUser GetUser(Guid userid)
        {
            return _userRepository.GetUser(userid);
        }
    }
}
