using Domain.Application.Features.SignUp.Interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Cms;


namespace Domain.Application.Features.SignUp.Repositories
{
    public class SignUpRequestRepository : ISignUpRequestRepository
    {
        protected readonly JobPortalDbContext _context;
        public SignUpRequestRepository(JobPortalDbContext dbContext)
        {
            _context = dbContext;
        }



        public Guid AddSignupRequest(SignUpRequest signUpRequest)
        {
            signUpRequest.Status = (int)Status.PENDING;
            _context.SignUpRequests.AddAsync(signUpRequest);
            _context.SaveChanges();
            return signUpRequest.Id;
        }

        public async Task<SignUpRequest> GetSignupRequestByIdAsync(Guid signupId)
        {
            return await _context.SignUpRequests.FirstOrDefaultAsync(x => x.Id == signupId);

        }
        public void UpdateSignupRequest(SignUpRequest signUpRequest)
        {
            _context.SignUpRequests.Update(signUpRequest);
            _context.SaveChanges();
        }


        public async Task<SignUpRequest> GetByEmailAsync(string email)
        {
            return await _context.SignUpRequests.FirstOrDefaultAsync(x => x.Email == email);
        }
        

    }
}

  
