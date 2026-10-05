using UseCases.Services.AuthServices.DTOs;
using UseCases.Services.SalesforceServices.DTOs.Recruter;

namespace ITransitionProject.PagesDTOs.Salesforce
{
    public class EditRecruterAccountFormPageDTO
    {
        public EditRecruterAccountDTO? PostedModel { get; } = null;
        public required RecruterAccountDTO Records { get; set; }
        public required AuthorizedUserDTO User { get; set; }
        public required Domain.Entities.Recruter Recruter { get; set; }
        public required string ActionForSubmit { get; set; }
        public required string ControllerForSubmit { get; set; }
    }
}
