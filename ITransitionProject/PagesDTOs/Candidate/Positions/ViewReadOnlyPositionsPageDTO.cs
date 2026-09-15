using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.Positions
{
    public class ViewReadOnlyPositionsPageDTO
    {
        public required IEnumerable<Position> Positions { get; set; }
        public string ActionForViewPosition { get; set; } = string.Empty;
        public string ControllerForViewPosition { get; set; } = string.Empty;
    }
}
