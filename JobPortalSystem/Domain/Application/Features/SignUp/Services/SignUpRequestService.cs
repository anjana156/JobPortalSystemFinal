using AutoMapper;
using Domain.Application.Features.Authuser.Interfaces;
using Domain.Application.Features.SignUp.DTO;
using Domain.Application.Features.SignUp.In;
using Domain.Application.Features.SignUp.Interfaces;
using Domain.Enums;
using Domain.Helpers;
using Domain.Infrastructure.ExternalServices;
using Domain.Models;
using Domain.Service;



namespace Domain.Application.Features.SignUp.Services
{
    public class SignUpRequestService : ISignUpRequestService
    {

        private readonly ISignUpRequestRepository _signUpRepository;
        private readonly IAuthUserRepository _authUserRepository;
        private readonly IMailService _emailService;
        private readonly IMapper _mapper;

        ISignUpRequestRepository signUpRepository;
        IAuthUserRepository authUserRepository;
        IMapper mapper;
        IMailService emailService;
        public SignUpRequestService(ISignUpRequestRepository _signUpRepository, IMapper _mapper, IMailService _emailService, IAuthUserRepository _authUserRepository)
        {
            signUpRepository = _signUpRepository;
            mapper = _mapper;
            emailService = _emailService;
            authUserRepository = _authUserRepository;
        }


        // SIGNUP

        public async Task CreateUserAccount(Guid signupId, string password)
        {
            try
            {
                SignUpRequest signUpRequest =
                    await signUpRepository.GetSignupRequestByIdAsync(signupId);

                if (signUpRequest == null)
                {
                    throw new Exception("Signup Request Not Found");
                }

                // NEW CHECK
                // Prevent using same signupId again
                if (signUpRequest.Status == Enums.Status.CREATED)
                {
                    throw new Exception("Account already created");
                }

                // EMAIL MUST BE VERIFIED
                if (signUpRequest.Status != Enums.Status.VERIFIED)
                {
                    throw new Exception("Email Not Verified");
                }

                // EXTRA SAFETY CHECK
                // Prevent duplicate email in AuthUser table
                var existingUser =
                    await authUserRepository.GetAuthUserByUserEmail(signUpRequest.Email);

                if (existingUser != null)
                {
                    throw new Exception("Email already exists");
                }

                AuthUser authUser = new();

                authUser.UserName = signUpRequest.UserName;
                authUser.FirstName = signUpRequest.FirstName;
                authUser.LastName = signUpRequest.LastName;
                authUser.Email = signUpRequest.Email;
                authUser.Password = password;
                authUser.Phone = signUpRequest.Phone;

                // ROLE BASED USER CREATION

                if (signUpRequest.Role == Enums.Role.JOB_SEEKER)
                {
                    authUser.Role = Enums.Role.JOB_SEEKER;

                    authUser = await authUserRepository.AddAuthUser(authUser);
                }
                else if (signUpRequest.Role == Enums.Role.JOB_PROVIDER)
                {
                    authUser.Role = Enums.Role.JOB_PROVIDER;

                    authUser = await authUserRepository.AddAuthUserJP(authUser);
                }
                else if (signUpRequest.Role == Enums.Role.COMPANY_USER)
                {
                    authUser.Role = Enums.Role.COMPANY_USER;

                    authUser = await authUserRepository.AddAuthUserJP(authUser);
                }
                else
                {
                    throw new Exception("Invalid Role");
                }

                // UPDATE STATUS
                signUpRequest.Status = Enums.Status.CREATED;

                signUpRepository.UpdateSignupRequest(signUpRequest);
            }
            catch (Exception)

            {
                throw;
            }
        }


        public async Task<Guid> CreateSignupRequest(SignUpRequestDto data)
        {
            // CHECK AUTHUSER TABLE
            var existingUser =
                await authUserRepository.GetAuthUserByUserEmail(data.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            // CHECK SIGNUPREQUEST TABLE
            var existingSignup =
                await signUpRepository.GetByEmailAsync(data.Email);

            if (existingSignup != null &&
                existingSignup.Status != Enums.Status.CREATED)
            {
                throw new Exception("Signup request already exists");
            }

            // MAP DTO TO MODEL
            var signUpRequest = mapper.Map<SignUpRequest>(data);

            // DEFAULT STATUS
            signUpRequest.Status = Enums.Status.PENDING;

            // SAVE
            var signUpId =
                signUpRepository.AddSignupRequest(signUpRequest);

            // SEND EMAIL
            MailRequest mailRequest = new MailRequest();
            mailRequest.Subject = "Job Portal Email Verification";
            mailRequest.Body = "http://localhost:4200/set-password?signupid=" + signUpId.ToString();
            mailRequest.ToEmail = signUpRequest.Email;
            await emailService.SendEmailAsync(mailRequest);
            return signUpId;
        }

        public async Task<bool> VerifyEmailAsync(Guid signupId)
        {
            var signupRequest =
                await signUpRepository.GetSignupRequestByIdAsync(signupId);

            if (signupRequest == null)
            {
                return false;
            }

            // IMPORTANT
            // Prevent verifying again
            if (signupRequest.Status == Enums.Status.CREATED)
            {
                throw new Exception("Account already created");
            }

            // Prevent verifying multiple times
            if (signupRequest.Status == Enums.Status.VERIFIED)
            {
                throw new Exception("Email already verified");
            }

            signupRequest.Status = Enums.Status.VERIFIED;

            signUpRepository.UpdateSignupRequest(signupRequest);

            return true;
        }



        }
}

