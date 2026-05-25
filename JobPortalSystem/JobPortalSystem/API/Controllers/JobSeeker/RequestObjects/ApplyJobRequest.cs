namespace JobPortalSystem.API.Controllers.JobSeeker.RequestObjects
{
    public class ApplyJobRequest
    {
        public Guid JobPost_id { get; set; }
        public Guid Applicant { get; set; }
        public Guid Resume_id { get; set; }
        public string CoverLetter { get; set; }
    }
}
