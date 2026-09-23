using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.CVs
{
    public class ViewCVsRecruterPageDTO
    {
        public IEnumerable<CV> CVs { get; set; }
        public IEnumerable<Position> Positions { get; set; }
        public string ActionForViewCV { get; set; } = string.Empty;
        public string ControllerForViewCV { get; set; } = string.Empty;
    }
}
