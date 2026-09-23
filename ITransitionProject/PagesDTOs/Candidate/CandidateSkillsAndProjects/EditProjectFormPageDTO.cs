using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkillsAndProjects
{
    public class EditProjectFormPageDTO
    {
        public required string ProjectIndex { get; set; }
        public required Project Project { get; set; }
        public required Dictionary<string, string> EventsHandlers { get; set; } = new();
        public required string ControllerForGetProjectTags { get; set; } 
        public required string ActionForGetProjectTags { get; set; } 
        public required string ActionForGetPopularProjectTags {  get; set; }
        public required string ControllerForGetPopularProjectTags { get; set; }
    }
}
