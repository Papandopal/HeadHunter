namespace ITransitionProject.PagesDTOs.Projects
{
    public class AddProjectPartialDTO
    {
        public int ProjectIndex { get; set; }
        public Dictionary<string, string> EventHandlers { get; set; } = new();
        public string ControllerForGetProjectTags { get; set; } = string.Empty;
        public string ActionForGetProjectTags { get; set; } = string.Empty;
    }
}
