using System.Text.Json;
using ITransitionProject.PagesDTOs.Support;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.CandidateServices.Interfaces;
using UseCases.Services.Entities.AdministratorServices.Interfaces;
using UseCases.Services.RecruterServices.Interfaces;
using UseCases.Services.SupportServices.DTOs;
using UseCases.Services.SupportServices.Interfaces;

namespace ITransitionProject.Controllers
{
    public class SupportController(IAuthService authService, ICandidateService candidateService, AlertService alertService,
        IRecruterService recruterService, IAdministratorService administratorService, ISupportService supportService) : Controller
    {
        private (string firstName, string lastName) GetBasicUserInfo(Guid userId)
        {
            var candidate = candidateService.GetItemOrDefaultByOwnerId(userId);
            if (candidate is not null) return (candidate.FirstName, candidate.LastName);

            var recruter = recruterService.GetItemOrDefaultByOwnerId(userId);
            if (recruter is not null) return (recruter.FirstName, recruter.LastName);

            throw new Exception("User not found");
        }

        [HttpGet]
        public IActionResult GetSupportTicketForm()
        {
            var user = authService.User();
            var basicUserInfo = GetBasicUserInfo(user.Id);
            var dto = new AddSupportTicketPageDTO
            {
                User = user,
                FirstName = basicUserInfo.firstName,
                LastName = basicUserInfo.lastName,
                AdministratorsEmails = administratorService.GetAdministratorsEmails(),
                ActionForSubmit = nameof(AddSupportTicketForm),
                ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
            };

            return View("AddSupportTicketForm", dto);
        }

        [HttpPost]
        public IActionResult AddSupportTicketForm(string json)
        {
            var addSupportTicketDTO = supportService.Deserialize(json);
            supportService.SendSupportTicket(addSupportTicketDTO);
            alertService.RaiseSuccess();
            return NoContent();
        }
    }
}
