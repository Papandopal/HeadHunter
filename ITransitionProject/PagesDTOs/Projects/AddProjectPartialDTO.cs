namespace ITransitionProject.PagesDTOs.Projects
{
    public class AddProjectPartialDTO
    {
        public int ProjectIndex { get; set; }
        public Dictionary<string, string> EventsHandlers { get; set; } = new();
        public required string ActionForGetPopularProjectTags { get; set; }
        public required string ControllerForGetPopularProjectTags { get; set; }
        public required string ActionForGetProjectTags { get; set; }
        public required string ControllerForGetProjectTags { get; set; }
    }
}
