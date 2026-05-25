using AutoMapper;
using Domain.Application.Features.Authuser.Interfaces;
using Domain.Application.Features.JobProvider.Interfaces;
using Domain.Application.Features.SignUp.DTO;
using Domain.Helpers;
using Domain.Infrastructure.ExternalServices;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobProvider.Repositories
{
    public class JobProviderService : IJobProviderService
    {
        IJobProviderRepository _jobProviderRepository;
        IMapper _mapper;
        IMailService _emailService;
        IAuthUserRepository _authUserRepository;
        public JobProviderService(IJobProviderRepository jobProviderRepository, IMapper mapper, IMailService emailService, IAuthUserRepository authUserRepository)
        {
            _jobProviderRepository = jobProviderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _authUserRepository = authUserRepository;
        }
        public async Task<List<JobPost>> GetJobs(Guid companyId)
        {
            return await _jobProviderRepository.GetJobs(companyId);
        }

        public async Task<List<JobPost>> GetAllJobsByProvider(Guid companyId, Guid jobproviderId)
        {
            return await _jobProviderRepository.GetAllJobsByProvider(companyId, jobproviderId);
        }

        public async Task<List<JobApplication>> GetAllJobApplicants(Guid jobproviderId)
        {
            return await _jobProviderRepository.GetAllJobApplicants(jobproviderId);
        }

        public Task<Guid> PostJob(JobPost job)
        {
            var id = _jobProviderRepository.Create(job);
            return id;
        }

        public async Task<JobPost> GetJobById(Guid jobId)
        {
            return await _jobProviderRepository.GetJobById(jobId);

        }
        public async Task<JobPost> Update(JobPost job, Guid id)
        {
            var updatedjob = await _jobProviderRepository.UpdateAsync(job, id);
            return updatedjob;
        }

        public void DeleteJob(Guid id)
        {
            _jobProviderRepository.DeleteJob(id);
        }

        public async void CreateSignupRequest(SignUpRequestDto data)
        {

            var signUpRequest = _mapper.Map<SignUpRequest>(data);
            var signUpId = _jobProviderRepository.AddSignupRequest(signUpRequest);
            MailRequest mailRequest = new MailRequest();
            mailRequest.Subject = "HireMeNow SignUp Verification";
            mailRequest.Body = "http://localhost:56067/set-password?signupid=" + signUpId.ToString();
            mailRequest.ToEmail = signUpRequest.Email;
            await _emailService.SendEmailAsync(mailRequest);
        }

        public async Task<bool> VerifyEmailAsync(Guid jobProviderSignupRequestId)
        {

            SignUpRequest signUpRequest = await _jobProviderRepository.GetSignupRequestByIdAsync(jobProviderSignupRequestId);
            if (signUpRequest != null)
            {
                signUpRequest.Status = Enums.Status.VERIFIED;
                _jobProviderRepository.UpdateSignupRequest(signUpRequest);
                return true;
            }
            return false;
        }

        public async Task CreateJobProvider(Guid jobProviderSignupRequestId, string password)
        {
            try
            {
                SignUpRequest signUpRequest = await _jobProviderRepository.GetSignupRequestByIdAsync(jobProviderSignupRequestId);
                //AuthUser authUser = mapper.Map<AuthUser>(signUpRequest);
                AuthUser authUser = new();
                if (signUpRequest.Status == Enums.Status.VERIFIED)
                {
                    //need to change this code by using Automapper 



                    authUser.UserName = signUpRequest.UserName;
                    authUser.Role = Enums.Role.JOB_PROVIDER;
                    authUser.FirstName = signUpRequest.FirstName;
                    authUser.LastName = signUpRequest.LastName;
                    authUser.Email = signUpRequest.Email;
                    authUser.Password = password;
                    authUser.Phone = signUpRequest.Phone;
                    authUser = await _authUserRepository.AddAuthUserJP(authUser);
                    signUpRequest.Status = Enums.Status.CREATED;
                    _jobProviderRepository.UpdateSignupRequest(signUpRequest);
                }

                Models.CompanyUser jobProvider = _mapper.Map<Models.CompanyUser>(authUser);

                //await jobSeekerRepository.AddJobSeekerAsync(jobseeker);
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public async Task<List<JobProviderCompany>> GetCompany(Guid jobproviderId)
        {
            return await _jobProviderRepository.GetCompany(jobproviderId);
        }
    }
}
