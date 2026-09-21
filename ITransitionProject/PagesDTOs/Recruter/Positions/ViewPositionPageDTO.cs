using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.Positions
{
    public class ViewPositionPageDTO
    {
        public required Position Position { get; set; }
        public required string ActionForEditPosition { get; set; }
        public required string ControllerForEditPosition { get; set; }  
    }
}
