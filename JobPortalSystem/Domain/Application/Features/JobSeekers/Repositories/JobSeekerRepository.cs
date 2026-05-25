using Domain.Application.Features.Authuser.DTO;
using Domain.Application.Features.JobSeeker.Interfaces;
using Domain.Application.Features.JobSeekers.DTO;
using Domain.Application.Features.Profile.DTO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobSeekers.Repositories
{
    public class JobSeekerRepository : IJobSeekerRepository
    {
        protected readonly JobPortalDbContext _context;
        public JobSeekerRepository(JobPortalDbContext context)
        {
            _context = context;
        }

        //PROFILE
        public async Task AddProfileAsync(JobSeekerProfile profile)
        {
            profile.Id = Guid.NewGuid();
            _context.JobSeekerProfiles.Add(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<JobSeekerProfile?> GetJobSeekerProfileByIds(Guid jobseekerId, Guid profileId)
        {
            return await _context.JobSeekerProfiles
             .FirstOrDefaultAsync(profile => profile.JobSeekerId == jobseekerId && profile.Id == profileId);
        }

        public async Task<JobSeekerProfile> GetProfileAsync(Guid jobSeekerId)
        {
            return await _context.JobSeekerProfiles
                        .Where(profile => profile.JobSeekerId == jobSeekerId)
                        .Include(profile => profile.Resume) // Include related entities if needed
                        .Include(profile => profile.JobSeekerProfileSkills) // Include related entities if needed

                        .Include(profile => profile.Qualifications) // Include related entities if needed
                        .Include(profile => profile.WorkExperiences) // Include related entities if needed
                        .FirstOrDefaultAsync();
        }

        public Task<JobSeekerProfile> GetProfiledetailAsync(Guid jobseekerId)
        {
            return _context.JobSeekerProfiles
          .Where(profile => profile.JobSeekerId == jobseekerId)
          .Include(profile => profile.Resume)
          .Include(profile => profile.JobSeekerProfileSkills)
          .Include(profile => profile.Qualifications)
          .Include(profile => profile.WorkExperiences)
          .FirstOrDefaultAsync();
        }

        public async Task<List<JobSeekerProfile>> GetProfilesByJobSeekerIdAsync(Guid jobSeekerId)
        {
            return await _context.JobSeekerProfiles
                .Where(profile => profile.JobSeekerId == jobSeekerId)
                .ToListAsync();
        }

        public List<JobSeekerProfileDto> GetProfile(Guid jobseekerId)
        {
            var jobSeekerProfile = _context.JobSeekerProfiles
           .Include(profile => profile.Qualifications)
           .Include(profile => profile.JobSeekerProfileSkills)
           .Include(profile => profile.JobSeeker)
           .FirstOrDefault(profile => profile.JobSeekerId == jobseekerId);

            if (jobSeekerProfile == null)
            {
                // Handle case when the profile is not found
                return new List<JobSeekerProfileDto>(); // or return null, depending on your handling
            }

            var jobSeekerProfileDTO = new JobSeekerProfileDto
            {
                UserName = jobSeekerProfile.JobSeeker.UserName,
                FirstName = jobSeekerProfile.JobSeeker.FirstName,
                LastName = jobSeekerProfile.JobSeeker.LastName,
                Phone = jobSeekerProfile.JobSeeker.Phone,
                Email = jobSeekerProfile.JobSeeker.Email,
                image = jobSeekerProfile.JobSeeker.Image,
                Qualification = jobSeekerProfile.Qualifications.ToList(),
                JobSeekerProfileSkills = jobSeekerProfile.JobSeekerProfileSkills.Select(s => s.Skill).ToList(),
                Role = jobSeekerProfile.JobSeeker.Role
            };

            // Return a list with a single item (your DTO)
            return new List<JobSeekerProfileDto> { jobSeekerProfileDTO };
        }

        public async Task<AuthUserDto> UpdateProfile(AuthUserDto updatedProfile)
        {
            // Retrieve the existing profiles
            var existingProfile = _context.AuthUsers.FirstOrDefault(e => e.Id == updatedProfile.JobseekerId);
            var existingProfile2 = _context.JobSeekers.FirstOrDefault(e => e.Id == updatedProfile.JobseekerId);

            if (existingProfile == null || existingProfile2 == null)
            {
                // Handle case when the profile is not found
                return null;
            }

            // Update image only if it's provided in the updatedProfile
            //if (updatedProfile.Image != null)
            //{
            //    byte[] byteArray = ConvertImageToByteArray(updatedProfile.Image);
            //    existingProfile2.Image = byteArray;
            //}

            // Update fields only if they are provided in the updatedProfile and different from the current values
            if (!string.IsNullOrEmpty(updatedProfile.FirstName))
            {
                existingProfile.FirstName = updatedProfile.FirstName;
                existingProfile2.FirstName = updatedProfile.FirstName;
            }

            if (!string.IsNullOrEmpty(updatedProfile.LastName))
            {
                existingProfile.LastName = updatedProfile.LastName;
                existingProfile2.LastName = updatedProfile.LastName;
            }

            if (!string.IsNullOrEmpty(updatedProfile.Phone))
            {
                existingProfile.Phone = updatedProfile.Phone;
                existingProfile2.Phone = updatedProfile.Phone;
            }

            if (!string.IsNullOrEmpty(updatedProfile.Password))
            {
                existingProfile.Password = updatedProfile.Password;
            }

            if (!string.IsNullOrEmpty(updatedProfile.UserName))
            {
                existingProfile.UserName = updatedProfile.UserName;
                existingProfile2.UserName = updatedProfile.UserName;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return updatedProfile;
        }

        //SKILLS

        public async Task AddSkillsToProfile(JobSeekerProfile profile)
        {
            if (profile != null)
            {
                //foreach (var skillName in skills)
                //{
                //    var skill = new Skill {Name = skillName }; // Adjust property name according to your Skill model
                //    profile.JobSeekerProfileSkills.Add(new JobSeekerProfileSkill { Skill = skill });
                //}

                _context.JobSeekerProfiles.Update(profile);
                _context.SaveChanges();
            }
        }

        public List<SkillDto> GetSkillsForProfile(Guid jobseekerId, Guid profileId)
        {
            return _context.JobSeekerProfiles
                   .Where(profile => profile.JobSeekerId == jobseekerId && profile.Id == profileId)
                   .SelectMany(profile => profile.JobSeekerProfileSkills.Select(skill => new SkillDto
                   {
                       Name = skill.Skill.Name,
                       Description = skill.Skill.Description
                   }))
                   .ToList();
        }

        public List<Skill> GetSkillsForProfile()
        {
            return _context.Skills.ToList();
        }

        //QUALIFICATION

        public async Task AddQualificationsToProfile(Guid profileId, Qualification qualification)
        {

            qualification.JobseekerProfileId = profileId;
            _context.Qualifications.Add(qualification);
            await _context.SaveChangesAsync();

        }

        public List<Qualification> GetQualification(Guid profileId)
        {
            return _context.Qualifications
                .Where(x => x.JobseekerProfileId == profileId)
                .ToList();
        }

        //EXPERIENCE

        public async Task AddWorkExperienceToProfile(Guid profileId, WorkExperience experience)
        {
            experience.JobSeekerProfileId = profileId;
            experience.Id = Guid.NewGuid();
            await _context.WorkExperiences.AddAsync(experience);
            await _context.SaveChangesAsync();

        }

        public List<WorkExperience> GetExperience(Guid jobseekerId, Guid profileId)
        {
            return _context.WorkExperiences
               .Where(experience => experience.JobSeekerProfileId == profileId)
               .ToList();


        }

        //RESUME

        public async Task AddResume(Guid resumeId, string title, byte[] fileData)
        {
            var newResume = new Resume
            {
                Id = resumeId,
                Title = title,
                File = fileData
            };

            _context.Resumes.Add(newResume);
            await _context.SaveChangesAsync();
        }

        public async Task AddResumeToProfile(Guid profileId, Guid resumeId, Guid jobSeekerId, string profileName, string profileSummary)
        {
            var newjobSeekerProfile = new JobSeekerProfile
            {
                Id = profileId,
                ResumeId = resumeId,
                JobSeekerId = jobSeekerId,
                ProfileName = profileName,
                ProfileSummary = profileSummary
            };

            _context.JobSeekerProfiles.Update(newjobSeekerProfile);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> GetResumeId(Guid profileId)
        {
            var jobSeekerProfile = _context.JobSeekerProfiles.FirstOrDefault(s => s.Id == profileId);
            Guid resumeId = jobSeekerProfile.ResumeId.Value;
            return resumeId;
        }


        public async Task<byte[]> GetResumeFile(Guid resumeId)
        {
            var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.Id == resumeId);
            if (resume == null)
            {
                return null; // or handle the case where the resume doesn't exist
            }
            return resume.File; // Assuming there's a property named ResumeData that contains the binary data.
        }

        public async Task UpdateResume(Guid resumeId, byte[] fileData)
        {
            var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.Id == resumeId);
            resume.File = fileData;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Resume>> GetResume(Guid resumeId)
        {
            return await _context.Resumes.Where(e => e.Id == resumeId).ToListAsync();
        }
        public async Task DeleteResume(Guid resumeId)
        {
            var resume = await _context.Resumes.FindAsync(resumeId);

            if (resume != null)
            {
                _context.Resumes.Remove(resume);
                await _context.SaveChangesAsync();
            }
        }
    }
}
