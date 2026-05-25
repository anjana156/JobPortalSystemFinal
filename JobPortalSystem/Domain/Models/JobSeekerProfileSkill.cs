using System;

namespace Domain.Models
{
        public class JobSeekerProfileSkill
        {
            public Guid Id { get; set; }
            public Guid JobSeekerProfileId { get; set; }
            public Guid SkillId { get; set; }
            public virtual JobSeekerProfile JobSeekerProfile { get; set; }
            public virtual Skill Skill { get; set; }
        }
    
}
