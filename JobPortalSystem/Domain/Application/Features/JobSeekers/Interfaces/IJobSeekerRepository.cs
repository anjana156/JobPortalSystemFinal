using Domain.Application.Features.Admin.DTO;
using Domain.Application.Features.Authuser.DTO;
using Domain.Application.Features.JobSeekers.DTO;
using Domain.Application.Features.Profile.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobSeeker.Interfaces
{
    public interface IJobSeekerRepository
    {
        // PROFILE

        Task AddProfileAsync(JobSeekerProfile profile);
        Task<JobSeekerProfile?> GetJobSeekerProfileByIds(Guid jobSeekerId, Guid profileId);
        Task<JobSeekerProfile> GetProfileAsync(Guid jobSeekerId);
        Task<JobSeekerProfile> GetProfiledetailAsync(Guid jobSeekerId);
        Task<List<JobSeekerProfile>> GetProfilesByJobSeekerIdAsync(Guid jobSeekerId);
        List<JobSeekerProfileDto> GetProfile(Guid jobSeekerId);

        Task<AuthUserDto> UpdateProfile(AuthUserDto updatedProfile);



        // SKILLS

        Task AddSkillsToProfile(JobSeekerProfile profile);
        List<Skill> GetSkillsForProfile();
        List<SkillDto> GetSkillsForProfile(Guid jobSeekerId, Guid profileId);


        // QUALIFICATION
        Task AddQualificationsToProfile(Guid profileId, Qualification qualification);
        List<Qualification> GetQualification(Guid profileId);

        // EXPERIENCE
        Task AddWorkExperienceToProfile(Guid profileId, WorkExperience experience);
        List<WorkExperience> GetExperience(Guid jobSeekerId, Guid profileId);


        // RESUME

        Task AddResume(Guid resumeId, string title, byte[] fileData);
        Task AddResumeToProfile(Guid profileId, Guid resumeId, Guid jobSeekerId, string profileName, string profileSummary);
        Task<Guid> GetResumeId(Guid profileId);
        Task<List<Resume>> GetResume(Guid resumeId);
        Task<byte[]> GetResumeFile(Guid resumeId);
        Task UpdateResume(Guid resumeId, byte[] fileData);
        Task DeleteResume(Guid resumeId);
    }
}