using Domain.Entities;


namespace ITransitionProject.PagesDTOs.Candidate.CVs
{
    public class ViewCVPageDTO
    {
        public required CV CV { get; set; }
        public required IEnumerable<Project> Projects { get; set; }
    }
}
