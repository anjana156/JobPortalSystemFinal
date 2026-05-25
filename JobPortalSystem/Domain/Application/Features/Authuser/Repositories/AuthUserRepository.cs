using AutoMapper;
using Domain.Application.Features.Authuser.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobSeekerEntity = Domain.Models.JobSeeker;




namespace Domain.Application.Features.Authuser.Repositories
{
    public class AuthUserRepository : IAuthUserRepository
    {
        protected readonly JobPortalDbContext _context;
        IMapper mapper;
        private readonly IConfiguration _configuration;

        public AuthUserRepository(JobPortalDbContext dbContext, IMapper _mapper, IConfiguration configuration)
        {
            _context = dbContext;
            mapper = _mapper;
            _configuration = configuration;
        }

        public async Task<AuthUser> AddAuthUser(AuthUser authUser)
        {
            authUser.Role = Enums.Role.JOB_SEEKER;
            await _context.AuthUsers.AddAsync(authUser);
            JobSeekerEntity jobSeeker = mapper.Map<JobSeekerEntity>(authUser);
            await _context.JobSeekers.AddAsync(jobSeeker);
            JobSeekerProfile profile = new();
            profile.JobSeekerId = jobSeeker.Id;
            await _context.JobSeekerProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return authUser;
        }

        public async Task<AuthUser> AddAuthUserJP(AuthUser authUser)
        {
            authUser.Role = Enums.Role.JOB_PROVIDER;
            await _context.AuthUsers.AddAsync(authUser);

            CompanyUser companyUser = mapper.Map<CompanyUser>(authUser);
            await _context.CompanyUsers.AddAsync(companyUser);
            await _context.SaveChangesAsync();
            return authUser;
        }
        public async Task<AuthUser> GetAuthUserByUserEmail(string email)
        {
            return await _context.AuthUsers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<AuthUser> GetAuthUserByUserId(Guid value)
        {
            return await _context.AuthUsers.FirstOrDefaultAsync(x => x.Id == value);
        }

        public string? CreateToken(AuthUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["AuthSettings:Token"]));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.Now.AddDays(1),
               signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
              return jwt;
        }

        public CompanyUser GetUser(Guid userid)
        {
            return _context.CompanyUsers.Where(e => e.Id == userid).FirstOrDefault();
        }


        //for chat application

        public async Task AddUserConnectionIdAsync(string email, string ConnectionId)
        {


            var userToUpdate = _context.AuthUsers.Where(e => e.Email == email).FirstOrDefault();
            if (userToUpdate != null)
            {
                userToUpdate.ConnectionId = ConnectionId;
                userToUpdate.OnlineStatus = true;

                //userToUpdate.LastActive=DateTime.Now;
                _context.AuthUsers.Update(userToUpdate);
                _context.SaveChanges();
            }

            //await _userRepository.Update(userToUpdate);
        }

        public Models.AuthUser GetUserByConnectionId(string connectionId)
        {

            return _context.AuthUsers.Where(x => x.ConnectionId == connectionId).FirstOrDefault();
        }

       
        public void DisconnectUserByConnectionId(string connectionId)
        {
            var userToUpdate = _context.AuthUsers.Where(e => e.ConnectionId == connectionId).FirstOrDefault();
            if (userToUpdate != null)
            {
                userToUpdate.ConnectionId = "";
                userToUpdate.OnlineStatus = false;
                //userToUpdate.LastActive=DateTime.Now;
                _context.AuthUsers.Update(userToUpdate);
                _context.SaveChanges();
            }
        }

    }
}
