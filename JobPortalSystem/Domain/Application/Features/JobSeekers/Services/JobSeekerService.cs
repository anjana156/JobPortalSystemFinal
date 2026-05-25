using AutoMapper;
using Domain.Application.Features.Authuser.DTO;
using Domain.Application.Features.JobSeeker.Interfaces;
using Domain.Application.Features.JobSeekers.DTO;
using Domain.Application.Features.JobSeekers.Repositories;
using Domain.Application.Features.Profile.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.Profile.Services
{
    public class JobseekerService : IJobSeekerService
    {
        public readonly IJobSeekerRepository _jobSeekerRepository;
        IMapper mapper;
        public JobseekerService(IJobSeekerRepository jobSeekerRepository, IMapper _mapper)
        {
            mapper = _mapper;
            _jobSeekerRepository = jobSeekerRepository;
        }

        //Profile
        public async Task<bool> AddProfileAsync(ProfileDto addProfileDto)
        {
            var profile = mapper.Map<JobSeekerProfile>(addProfileDto);
            await _jobSeekerRepository.AddProfileAsync(profile);
            return true;
        }

        public Task<JobSeekerProfile> GetProfileAsync(Guid jobSeekerId)
        {
            return _jobSeekerRepository.GetProfileAsync(jobSeekerId);


        }
        public async Task<JobSeekerProfileDto> GetcompleteProfile(Guid jobseekerId)
        {
            var jobSeekerProfile = await _jobSeekerRepository.GetProfiledetailAsync(jobseekerId);

            if (jobSeekerProfile == null)
            {
                // Handle case when the profile is not found
                return null; // or throw an exception or handle it according to your application logic
            }

            var jobSeekerProfileDTO = new JobSeekerProfileDto
            {
                UserName = jobSeekerProfile.JobSeeker.UserName,
                FirstName = jobSeekerProfile.JobSeeker.FirstName,
                LastName = jobSeekerProfile.JobSeeker.LastName,
                Phone = jobSeekerProfile.JobSeeker.Phone,
                Email = jobSeekerProfile.JobSeeker.Email,
                Qualification = jobSeekerProfile.Qualifications.ToList(),
                JobSeekerProfileSkills = jobSeekerProfile.JobSeekerProfileSkills.Select(s => s.Skill).ToList(),
                Role = jobSeekerProfile.JobSeeker.Role,

            };

            return jobSeekerProfileDTO;
        }

        public Task GetProfileDetailsAsync(Guid jobseekerId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<JobSeekerProfile>> GetProfilesByJobSeekerIdAsync(Guid jobSeekerId)
        {
            return await _jobSeekerRepository.GetProfilesByJobSeekerIdAsync(jobSeekerId);
        }

        public List<JobSeekerProfileDto> GetProfile(Guid jobseekerId)
        {
            return _jobSeekerRepository.GetProfile(jobseekerId);
        }

        public async Task<AuthUserDto> UpdateJobSeekerProfile(AuthUserDto updatedProfile)
        {
            // Perform validation, mapping, and update logic if needed
            // Call the repository to update the JobSeeker's profile
            var result = await _jobSeekerRepository.UpdateProfile(updatedProfile);

            return result;
        }

        // Skills
        public async Task AddSkillsToProfile(Guid jobseekerId, Guid profileId, List<Guid> skills)
        {
            var profile = await _jobSeekerRepository.GetJobSeekerProfileByIds(jobseekerId, profileId);

            if (profile != null)
            {
                List<JobSeekerProfileSkill> skillslist = new List<JobSeekerProfileSkill>();
                skills.ForEach(x =>
                {
                    JobSeekerProfileSkill jobSeekerProfileSkill = new JobSeekerProfileSkill();

                    jobSeekerProfileSkill.SkillId = x;
                    jobSeekerProfileSkill.JobSeekerProfileId = profile.Id;
                    skillslist.Add(jobSeekerProfileSkill);
                });
                profile.JobSeekerProfileSkills = skillslist;
                // Add the skills to the profile
                await _jobSeekerRepository.AddSkillsToProfile(profile);
            }
            else
            {
                throw new Exception("Profile not found");
            }
        }

        public List<SkillDto> GetSkillsForJobSeekerProfile(Guid jobseekerId, Guid profileId)
        {
            return _jobSeekerRepository.GetSkillsForProfile(jobseekerId, profileId);
        }

        public List<SkillDto> GetSkillsForJobSeekerProfile()
        {
            var Skills = _jobSeekerRepository.GetSkillsForProfile();
            var SkillDtos = mapper.Map<List<SkillDto>>(Skills);

            return SkillDtos;

        }

        // Qualification

        public Task AddQualificationToProfileAsync(Guid jobseekerId, Guid profileId, QualificationDto jobseekerQualificationDTo)
        {
            var profile = _jobSeekerRepository.GetJobSeekerProfileByIds(jobseekerId, profileId);
            if (profile != null)
            {
                var Qualification = mapper.Map<Qualification>(jobseekerQualificationDTo);
                return _jobSeekerRepository.AddQualificationsToProfile(profileId, Qualification);

            }
            else
            {
                throw new Exception("Profile not found");
            }
        }

        public List<QualificationDto> GetQualification(Guid profileId)
        {

            var Qualifications = _jobSeekerRepository.GetQualification(profileId);
            var QualificationDtos = mapper.Map<List<QualificationDto>>(Qualifications);

            return QualificationDtos;

        }


        //Experience

        public async Task AddWorkExpericeToProfileAsync(Guid jobseekerId, Guid profileId, ExperienceDto data)
        {
            //var profile = _profileRepository.GetJobSeekerProfileByIds(jobseekerId, profileId);
            //if (profile != null)
            //{
            var Experience = mapper.Map<WorkExperience>(data);
            await _jobSeekerRepository.AddWorkExperienceToProfile(profileId, Experience);


        }

        public List<ExperienceDto> GetExperience(Guid jobseekerId, Guid profileId)
        {

            var workExperiences = _jobSeekerRepository.GetExperience(jobseekerId, profileId);
            var experienceDtos = mapper.Map<List<ExperienceDto>>(workExperiences);

            return experienceDtos;

        }

        // Resume

        public async Task<Guid> AddResume(string title, byte[] fileData)
        {
            Guid resumeId = Guid.NewGuid();
            await _jobSeekerRepository.AddResume(resumeId, title, fileData);

            return resumeId;
        }

        public async Task AddResumeToProfile(Guid profileId, Guid resumeId, Guid jobSeekerId, string profileName, string profileSummary)
        {
            await _jobSeekerRepository.AddResumeToProfile(profileId, resumeId, jobSeekerId, profileName, profileSummary);
        }
        public async Task<Guid> GetResumeId(Guid profileId)
        {
            Guid resumeId = await _jobSeekerRepository.GetResumeId(profileId);
            return resumeId;
        }

        public async Task<List<Resume>> GetResumeById(Guid resumeId)
        {
            return await _jobSeekerRepository.GetResume(resumeId);
        }

        public async Task<byte[]> GetResumeFile(Guid resumeId)
        {
            byte[] byteArray = await _jobSeekerRepository.GetResumeFile(resumeId);
            return byteArray;
        }

        public async Task UpdateResume(Guid resumeId, byte[] fileData)
        {
            await _jobSeekerRepository.UpdateResume(resumeId, fileData);
        }

       
        public async Task DeleteResume(Guid resumeId)
        {
            await _jobSeekerRepository.DeleteResume(resumeId);
        }

    }
}  
