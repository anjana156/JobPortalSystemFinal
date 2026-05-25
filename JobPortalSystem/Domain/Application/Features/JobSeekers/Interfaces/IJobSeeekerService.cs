using Domain.Application.Features.Authuser.DTO;
using Domain.Application.Features.JobSeekers.DTO;
using Domain.Application.Features.Profile.DTO;
using Domain.Models;

namespace Domain.Application.Features.JobSeeker.Interfaces
{
    public interface IJobSeekerService
    {
        // PROFILE
        Task<bool> AddProfileAsync(ProfileDto addProfileDto);
        Task<JobSeekerProfile> GetProfileAsync(Guid jobSeekerId);
        Task<JobSeekerProfileDto> GetcompleteProfile(Guid jobSeekerId);


        Task GetProfileDetailsAsync(Guid jobSeekerId);
        Task<List<JobSeekerProfile>> GetProfilesByJobSeekerIdAsync(Guid jobSeekerId);
        List<JobSeekerProfileDto> GetProfile(Guid jobSeekerId);

        Task<AuthUserDto> UpdateJobSeekerProfile(AuthUserDto updatedProfile);


        // SKILLS
        Task AddSkillsToProfile(Guid jobSeekerId, Guid profileId, List<Guid> skills);
        List<SkillDto> GetSkillsForJobSeekerProfile(Guid jobSeekerId, Guid profileId);
        List<SkillDto> GetSkillsForJobSeekerProfile();



        // QUALIFICATION
        Task AddQualificationToProfileAsync(Guid jobSeekerId, Guid profileId, QualificationDto qualificationDto);
        List<QualificationDto> GetQualification(Guid profileId);


        // EXPERIENCE
        Task AddWorkExpericeToProfileAsync(Guid jobSeekerId, Guid profileId, ExperienceDto experienceDto);
        List<ExperienceDto> GetExperience(Guid jobSeekerId, Guid profileId);


        // RESUME
        Task<Guid> AddResume(string title, byte[] fileData);
        Task AddResumeToProfile(Guid profileId, Guid resumeId, Guid jobSeekerId, string profileName, string profileSummary);
        Task<Guid> GetResumeId(Guid profileId);
        Task<List<Resume>> GetResumeById(Guid resumeId);
        Task<byte[]> GetResumeFile(Guid resumeId);
        Task UpdateResume(Guid resumeId, byte[] fileData);
        Task DeleteResume(Guid resumeId);
    }
}