using Infrastructure.Migrations;
using UseCases.Services.AuthServices.DTOs;
using UseCases.Services.SupportServices.DTOs;

namespace ITransitionProject.PagesDTOs.Support
{
    public class AddSupportTicketPageDTO
    {
        public AddSupportTicketDTO? PostedModel { get; } = null;
        public required AuthorizedUserDTO User { get;set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required IEnumerable<string> AdministratorsEmails { get; set; }
        public required string ActionForSubmit { get; set; }
        public required string ControllerForSubmit { get; set; }
    }
}
