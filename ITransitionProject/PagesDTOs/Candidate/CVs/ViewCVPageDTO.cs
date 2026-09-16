using Domain.Entities;


namespace ITransitionProject.PagesDTOs.Candidate.CVs
{
    public class ViewCVPageDTO
    {
        public Domain.Entities.Candidate Candidate { get; set; }
        public CV CV { get; set; }
    }
}
