using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Administrator.Positions
{
    public class ViewPositionsAdministratorPageDTO
    {
        public IEnumerable<Position> Positions { get; set; }
        public required string ActionForDeletePositions { get; set; } 
        public required string ControllerForDeletePositions { get; set; } 
        public required string ActionForViewPosition {  get; set; } 
        public required string ControllerForViewPosition { get; set; }
    }
}
