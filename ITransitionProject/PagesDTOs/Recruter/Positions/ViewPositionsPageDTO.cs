using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.Positions
{
    public class ViewPositionsPageDTO
    {
        public IEnumerable<Position> Positions { get; set; }
        public string ActionForDeletePositions { get; set; } = string.Empty;
        public string ControllerForDeletePositions { get; set; } = string.Empty;
        public string ActionForEditPosition {  get; set; } = string.Empty;
        public string ControllerForEditPosition { get; set; } = string.Empty;
    }
}
