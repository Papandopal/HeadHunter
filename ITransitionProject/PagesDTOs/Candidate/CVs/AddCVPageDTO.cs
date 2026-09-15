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
        public string ActionForSubmit { get; set; } = string.Empty;
        public string ControllerForSubmit { get; set; } = string.Empty;
    }
}
