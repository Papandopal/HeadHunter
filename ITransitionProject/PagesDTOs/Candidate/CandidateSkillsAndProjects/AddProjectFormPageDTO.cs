namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkillsAndProjects
{
    public class AddProjectFormPageDTO
    {
        public required int ProjectIndex { get; set; }
        public Dictionary<string, string> EventsHandlers { get; set; } = new();
        public required string ControllerForGetProjectTags { get; set; } 
        public required string ActionForGetProjectTags { get; set; } 
        public required string ActionForGetPopularProjectTags { get; set; }
        public required string ControllerForGetPopularProjectTags { get; set; }
    }
}
