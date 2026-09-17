using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Projects
{
    public class EditProjectPartialDTO
    {
        public string ProjectIndex;
        public Project Project { get; set; }
        public Dictionary<string, string> EventsHandlers { get; set; }
        public string ActionForGetProjectTags { get; set; } = string.Empty;
        public string ControllerForGetProjectTags { get; set; } = string.Empty; 
    }
}
