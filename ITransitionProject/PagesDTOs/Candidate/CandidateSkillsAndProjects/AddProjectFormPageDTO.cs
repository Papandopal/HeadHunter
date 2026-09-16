namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkillsAndProjects
{
    public class AddProjectFormPageDTO
    {
        public int ProjectIndex { get; set; }
        public Dictionary<string, string> EventsHandlers { get; set; } = new();
        public string ControllerForGetProjectTags { get; set; } = string.Empty;
        public string ActionForGetProjectTags { get; set; } = string.Empty;
    }
}
