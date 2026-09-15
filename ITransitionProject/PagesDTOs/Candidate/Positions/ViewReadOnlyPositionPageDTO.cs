using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.Positions
{
    public class ViewReadOnlyPositionPageDTO
    {
        public Position Position { get; set; }
        public string ActionForGenerateCV { get; set; } = string.Empty;
        public string ControllerForGenerateCV { get; set; } = string.Empty;
    }
}
