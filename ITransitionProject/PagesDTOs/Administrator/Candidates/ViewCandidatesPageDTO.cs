using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Administrator.Candidates
{
    public class ViewCandidatesPageDTO
    {
        public required IEnumerable<Domain.Entities.Candidate> Candidates { get; set; }
        public required IDictionary<Guid, bool> BlockedCandidates { get; set; }
        public required string ActionForViewCandidateProfile { get; set; }
        public required string ControllerForViewCandidateProfile { get; set; }
        public required string ActionForDeleteCandidates {  get; set; }
        public required string ControllerForDeleteCandidates { get; set; }
        public required string ActionForBlockCandidates { get; set; }
        public required string ControllerForBlockCandidates { get; set; }
        public required string ActionForUnblockCandidates { get; set; }
        public required string ControllerForUnblockCandidates { get; set; }
    }
}
