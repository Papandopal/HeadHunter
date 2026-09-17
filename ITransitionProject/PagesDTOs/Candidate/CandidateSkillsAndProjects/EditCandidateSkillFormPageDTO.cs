using UseCases.Services.ValuedSkillServices.GeneralDTOs;

namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkillsAndProjects
{
    public class EditCandidateSkillFormPageDTO
    {
        public required ValuedSkillDTO ValuedSkill { get; set; }
        public required Dictionary<string, string> EventsHandlers { get; set; }
    }
}
