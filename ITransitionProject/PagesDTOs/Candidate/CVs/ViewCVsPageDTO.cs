using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.CVs
{
    public class ViewCVsPageDTO
    {
        public IEnumerable<CV> CVs { get; set; }
        public IEnumerable<Position> Positions { get; set; }
        public string ActionForViewCV {  get; set; } = string.Empty;
        public string ControllerForViewCV { get; set; } = string.Empty;
    }
}
