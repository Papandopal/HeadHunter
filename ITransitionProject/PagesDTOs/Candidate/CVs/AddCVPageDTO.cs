using Domain.Entities;
using UseCases.Services.CVServices.DTOs;

namespace ITransitionProject.PagesDTOs.Candidate.CVs
{
    public class AddCVPageDTO
    {
        public Guid PositionId { get; set; }
        public AddCVDTO? PostedModel { get; } = null;
        public IEnumerable<CandidateSkill> ValuedSkills { get; set; }
        public IEnumerable<Skill> NotValuedSkills { get; set; }
        public int CountOfRequiredProperties => NotValuedSkills.Count();
        public required string ActionForUploadImage { get; set; }
        public required string ControllerForUploadImage { get; set; }
        public required string ActionForSubmit { get; set; } 
        public required string ControllerForSubmit { get; set; }
    }
}
