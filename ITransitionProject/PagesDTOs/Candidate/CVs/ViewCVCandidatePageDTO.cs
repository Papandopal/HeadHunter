using Domain.Entities;


namespace ITransitionProject.PagesDTOs.Candidate.CVs
{
    public class ViewCVCandidatePageDTO
    {
        public required CV CV { get; set; }
        public required IEnumerable<CandidateSkill> CandidateSkills { get; set; }
        public required IEnumerable<Project> Projects { get; set; }
        public required string ActionForEditCV { get; set; }
        public required string ControllerForEditCV { get; set; }
    }
}
