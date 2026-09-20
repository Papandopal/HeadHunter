using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.Positions
{
    public class ViewPositionsPageDTO
    {
        public IEnumerable<Position> Positions { get; set; }
        public required string ActionForDeletePositions { get; set; } 
        public required string ControllerForDeletePositions { get; set; } 
        public required string ActionForEditPosition {  get; set; } 
        public required string ControllerForEditPosition { get; set; }
    }
}
