using AutoMapper;
using Domain.Application.Features.Admin.DTO;
using Domain.Application.Features.Job.DTO;
using Domain.Application.Features.JobProvider.DTO;
using Domain.Application.Features.JobSeekers.DTO;
using Domain.Application.Features.Login.DTO;
using Domain.Application.Features.Profile.DTO;
using Domain.Application.Features.SignUp.DTO;
using Domain.Models;
using Domain.Service.Login.DTOs;
using JobPortalSystem.API.Controllers.Admin.RequestObjects;
using JobPortalSystem.API.Controllers.CompanyUser.RequestObjects;
using JobPortalSystem.API.Controllers.JobSeeker.RequestObjects;
using JobProviderDto = Domain.Application.Features.Admin.DTO.JobProviderDto;

namespace JobPortalSystem.API.Extensions
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<SignUpRequestDto, SignUpRequest>();
            CreateMap<SignUpRequest, AuthUser>();
            CreateMap<AuthUser, LoginRequestDto>();
            CreateMap<AuthUser, LoginResponseDto>()
                .ForMember
                (
                    dest => dest.Role,
                    opt => opt.MapFrom(src => src.Role.ToString())
                );



            CreateMap<SignUpRequest, SystemUser>().ReverseMap();
            CreateMap<AuthUser, Domain.Models.JobSeeker>().ReverseMap();
            CreateMap<AuthUser, SystemUser>().ReverseMap();
            CreateMap<AuthUser, Domain.Models.CompanyUser>().ReverseMap();
            //CreateMap<JobPost, JobPostsDtos>().ReverseMap();
            CreateMap<JobPost, JobProviderDto>().ReverseMap();
            //CreateMap<Qualification,QualificationsRequestDto>().ReverseMap();
            //CreateMap<QualificationRequest, JobseekerQualificationDTo>();
            //CreateMap<Qualification,JobseekerQualificationDTo>();
            CreateMap<Skill, SkillDto>();
            CreateMap<QualificationDto, Qualification>();
            CreateMap<AddExperienceRequest,ExperienceDto>();
            CreateMap<ExperienceDto, WorkExperience>();
            CreateMap<WorkExperience, ExperienceDto>();
            CreateMap<AuthUser, JobSeekerLoginDto>();

            CreateMap<SkillRequest, SkillDto>();
            CreateMap<IndustryRequest, Industry>();
            CreateMap<LocationRequest, Location>();



            CreateMap<Industry, IndustryRequest>().ReverseMap();
            CreateMap<JobCategory, CategoryRequest>().ReverseMap();
            CreateMap<Location, LocationRequest>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();

            //CreateMap<CompanyMemberDtos, CompanyUser>().ReverseMap();
            //CreateMap<companyUserRequest, CompanyMemberDtos>().ReverseMap();

            //CreateMap<CompanyMemberDtos, AuthUser>().ReverseMap();
            //CreateMap<JobPostRequest, JobPost>().ReverseMap();

            //CreateMap<JobApplication, JobApplicationDto>().ReverseMap();
            CreateMap<JobProviderCompany, JobProviderDto>().ReverseMap();


            //CreateMap<AuthUser, JobSeekerLoginDto>();
            CreateMap<JobPost, Joblist>().ReverseMap();


            CreateMap<JobSeekerProfileDto,JobSeeker>();
            CreateMap<ApplyJobRequest, JobApplication>();
            CreateMap<JobApplication, AppliedJobsDtos>();
            CreateMap<CompanyRegistrationDtos, JobProviderCompany>().ReverseMap();
            CreateMap<AddCompanyRequestobject, JobProviderCompany>().ReverseMap();
            CreateMap<CompanyRegistrationDtos, AddCompanyRequestobject>().ReverseMap();
            CreateMap<CompanyUpdateDtos, CompanyupdateRequest>().ReverseMap();
            CreateMap<CompanyUpdateDtos, JobProviderCompany>().ReverseMap();
            CreateMap<SavedJob, SavedJobsDtos>().ReverseMap();
            CreateMap<JobProviderCompany, GetCompanyDetailsDto>();
            CreateMap<InterviewSheduleObject, InterviewsheduleDtos>();
            CreateMap<InterviewsheduleDtos, Interview>();
            CreateMap<SheduledInterviewDto, Interview>();
            CreateMap<Interview, SheduledInterviewDto>();
            CreateMap<CompanyUser, CompanyMemberListDtos>().ReverseMap();
            CreateMap<SavedJobRequest, SavedJob>().ReverseMap();



            CreateMap<JobPost, JobProviderDto>().ReverseMap();
            CreateMap<Domain.Models.JobSeeker, JobSeekerDto>().ReverseMap();
          
            CreateMap<CompanyUser, CompanyUsersDto>().ReverseMap();
            CreateMap<Resume, ResumeDto>();
            CreateMap<JobSeekerProfile, ProfileDto>();
            
            CreateMap<ProfileDto, JobSeekerProfile>();
            CreateMap<SkillRequest, SkillDto>();
            CreateMap<SkillDto, Skill>();

            //CreateMap<AuthUser, ChatUserDto>().ReverseMap();
        }
    }
}

