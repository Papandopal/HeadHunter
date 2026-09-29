using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.CVs
{
    public class ViewCVsCandidatePageDTO
    {
        public required IEnumerable<CV> CVs { get; set; }
        public required IEnumerable<Position> Positions { get; set; }
        public required string ActionForViewCV {  get; set; } 
        public required string ControllerForViewCV { get; set; }
        public required string ActionForDeleteCVs { get; set; } 
        public required string ControllerForDeleteCVs { get; set; }
    }
}
