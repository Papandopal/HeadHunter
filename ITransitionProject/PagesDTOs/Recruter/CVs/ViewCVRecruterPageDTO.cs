using Domain.Entities;


namespace ITransitionProject.PagesDTOs.Recruter.CVs
{
    public class ViewCVRecruterPageDTO
    {
        public required CV CV { get; set; }
        public required IEnumerable<CandidateSkill> CandidateSkills { get; set; }
        public required IEnumerable<Project> Projects { get; set; }
        public required string ActionForLike { get; set; }
        public required string ControllerForLike { get; set; }
        public required string ActionForUnlike { get; set; }
        public required string ControllerForUnlike { get; set; }
        public required bool IsCVLiked { get; set; }
    }
}
