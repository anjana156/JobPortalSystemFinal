using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.SignUp.Interfaces
{
    public interface ISignUpRequestRepository
    {


        Guid AddSignupRequest(SignUpRequest signUpRequest);
        Task<SignUpRequest> GetSignupRequestByIdAsync(Guid signupId);
        void UpdateSignupRequest(SignUpRequest signUpRequest);
        Task<SignUpRequest> GetByEmailAsync(string email);

        //Task AddJobSeekerAsync(Models.JobSeeker jobseeker);

        
    }
}

