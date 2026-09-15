using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.CVs
{
    public class AddCVPageDTO
    {
        public IEnumerable<CandidateSkill> ValuedSkills { get; set; }
        public IEnumerable<Skill> NotValuedSkills { get; set; }
    }
}
