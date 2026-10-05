using Domain.Entities;
using UseCases.Services.AuthServices.DTOs;
using UseCases.Services.SalesforceServices.DTOs.Candidate;

namespace ITransitionProject.PagesDTOs.Salesforce
{
    public class AddCandidateAccountFormPageDTO
    {
        public AddCandidateAccountDTO? PostedModel { get; } = null;
        public required AuthorizedUserDTO User { get; set; }
        public required Domain.Entities.Candidate Candidate { get; set; }
        public required string ActionForSubmit { get; set; }
        public required string ControllerForSubmit { get; set; }
    }
}
