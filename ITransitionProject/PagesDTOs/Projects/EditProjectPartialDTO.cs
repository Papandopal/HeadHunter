using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Projects
{
    public class EditProjectPartialDTO
    {
        public required string ProjectIndex;
        public required Project Project { get; set; }
        public Dictionary<string, string> EventsHandlers { get; set; } = new();
        public required string ActionForGetPopularProjectTags { get; set; }
        public required string ControllerForGetPopularProjectTags { get; set; }
        public required string ActionForGetProjectTags { get; set; } 
        public required string ControllerForGetProjectTags { get; set; } 
    }
}
