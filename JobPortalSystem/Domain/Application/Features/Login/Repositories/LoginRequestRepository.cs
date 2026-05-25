using Domain.Application.Features.Login.Interfaces;
using Domain.Models;
using System;
using Domain.Application.Features.Authuser.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Domain.Application.Features.Login.Repositories
{
    public class LoginRequestRepository : ILoginRequestRepository
    {
        protected readonly JobPortalDbContext _context;
        public LoginRequestRepository(JobPortalDbContext dbContext)
        {
            _context = dbContext;
        }

        public AuthUser GetUserByEmail(string email)
        {
            var user = _context.AuthUsers.FirstOrDefault(e => e.Email == email);
            return user;
        }


        public AuthUser GetUserByEmailpassword(string email, string password)
        {
            var user = _context.AuthUsers.FirstOrDefault(e => e.Email == email && e.Password == password);
            return user;
        }
    }

}

