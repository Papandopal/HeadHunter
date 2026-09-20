using Domain.Entities;
using UseCases.Services.Entities.CVServices.DTOs;

namespace ITransitionProject.PagesDTOs.Candidate.CVs
{
    public class EditCVPageDTO
    {
        public EditCVDTO? PostedModel { get; } = null;
        public required CV CV { get; set; }
        public required string ActionForGetProjectTags { get; set; }
        public required string ControllerForGetProjectTags { get; set; }
        public required string ActionForUploadImage { get; set; }
        public required string ControllerForUploadImage { get; set; }
        public required string ActionForSubmit { get; set; } 
        public required string ControllerForSubmit { get; set; } 
    }
}
