using Domain.Models;



namespace Domain.Application.Features.Authuser.Interfaces
{
    public interface IAuthUserRepository
    {

        Task<Models.AuthUser> AddAuthUser(Models.AuthUser authUser);

        Task<Models.AuthUser> AddAuthUserJP(Models.AuthUser authUser);
        string? CreateToken(Models.AuthUser user);
        Task<Models.AuthUser> GetAuthUserByUserEmail(string user);
        Task<Models.AuthUser> GetAuthUserByUserId(Guid value);


        CompanyUser GetUser(Guid userid);
        Task AddUserConnectionIdAsync(string email, string ConnectionId);
        Models.AuthUser GetUserByConnectionId(string connectionId);
        void DisconnectUserByConnectionId(string connectionId);

    }
}
