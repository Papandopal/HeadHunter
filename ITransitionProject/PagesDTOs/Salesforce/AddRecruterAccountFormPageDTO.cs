using UseCases.Services.AuthServices.DTOs;
using UseCases.Services.SalesforceServices.DTOs.Recruter;

namespace ITransitionProject.PagesDTOs.Salesforce
{
    public class AddRecruterAccountFormPageDTO
    {
        public AddRecruterAccountDTO? PostedModel { get; } = null;
        public required AuthorizedUserDTO User { get; set; }
        public required Domain.Entities.Recruter Recruter { get; set; }
        public required string ActionForSubmit { get; set; }
        public required string ControllerForSubmit { get; set; }
    }
}
