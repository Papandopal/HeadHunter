using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.Positions
{
    public class ViewPositionsRecruterPageDTO
    {
        public IEnumerable<Position> Positions { get; set; }
        public required string ActionForAddPosition { get; set; }
        public required string ControllerForAddPosition { get; set; }
        public required string ActionForDeletePositions { get; set; } 
        public required string ControllerForDeletePositions { get; set; } 
        public required string ActionForViewPosition {  get; set; } 
        public required string ControllerForViewPosition { get; set; }
    }
}
