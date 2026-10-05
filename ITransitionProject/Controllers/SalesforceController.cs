using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Database.Exceptions;
using ITransitionProject.PagesDTOs.Salesforce;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.CandidateServices.Interfaces;
using UseCases.Services.Exceptions;
using UseCases.Services.RecruterServices.Interfaces;
using UseCases.Services.SalesforceServices.DTOs.Candidate;
using UseCases.Services.SalesforceServices.DTOs.Recruter;
using UseCases.Services.SalesforceServices.Interfaces;

namespace ITransitionProject.Controllers
{
    public class SalesforceController(ICandidateService candidateService, AlertService alertService, IRecruterService recruterService,
        IAuthService authService, ISalesforceService salesforceService) : Controller
    {
        private Candidate? Candidate()
        {
            return candidateService.GetItemOrDefaultByOwnerId(authService.User().Id);
        }

        private Recruter? Recruter()
        {
            return recruterService.GetItemOrDefaultByOwnerId(authService.User().Id);
        }

        private IActionResult ValidationDecorator(Func<IActionResult> action, string reconnectActionName)
        {
            try
            {
                if (!authService.Validate()) throw new FailedAuthValidationException("User blocked");
                if (Candidate() is null && Recruter() is null) throw new FailedAuthValidationException("User not created");
                return action.Invoke();
            }
            catch (NotEqualItemVersionException ex)
            {
                alertService.RaiseAlert(ex.Message, AlertTypes.Danger);
                return RedirectToAction(reconnectActionName);
            }
            catch (FailedAuthValidationException ex)
            {
                alertService.RaiseAlert(ex.Message, AlertTypes.Danger);
                return RedirectToAction(nameof(CandidateController.Home), nameof(CandidateController).Replace("Controller", ""));
            }
            catch (Exception ex)
            {
                alertService.RaiseAlert("Something wrong...", AlertTypes.Warning);
                return RedirectToAction("Logout", "Auth");
            }
        }

        private async Task<IActionResult> ValidationDecoratorAsync(Func<Task<IActionResult>> action, string reconnectActionName)
        {
            try
            {
                if (!authService.Validate()) throw new FailedAuthValidationException("User blocked");
                if (Candidate() is null && Recruter() is null) throw new FailedAuthValidationException("User not created");
                return await action.Invoke();
            }
            catch (NotEqualItemVersionException ex)
            {
                alertService.RaiseAlert(ex.Message, AlertTypes.Danger);
                return RedirectToAction(reconnectActionName);
            }
            catch (FailedAuthValidationException ex)
            {
                alertService.RaiseAlert(ex.Message, AlertTypes.Danger);
                return RedirectToAction("Logout", "Auth");
            }
            catch (Exception ex)
            {
                alertService.RaiseAlert("Something wrong...", AlertTypes.Warning);
                return RedirectToAction("Logout", "Auth");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AccountForm()
        {
            return await ValidationDecoratorAsync(async () =>
            {
                if (Candidate() is not null)
                {
                    var candidate = await salesforceService.TryGetCandidateAccountAsync();
                    if (candidate is null) return RedirectToAction(nameof(GetCandidateAccountCreateForm));
                    else return RedirectToAction(nameof(GetCandidateAccountEditForm), candidate);
                }
                else
                {
                    var recruter = await salesforceService.TryGetRecruterAccountAsync();
                    if (recruter is null) return RedirectToAction(nameof(GetRecruterAccountCreateForm));
                    else return RedirectToAction(nameof(GetRecruterAccountEditForm), recruter);
                }
            }, nameof(AccountForm));
        }

        [HttpGet]
        public IActionResult GetCandidateAccountCreateForm()
        {
            var candidate = Candidate();
            var user = authService.User();
            var dto = new AddCandidateAccountFormPageDTO
            {
                Candidate = candidate,
                User = user,
                ActionForSubmit = nameof(CreateCandidateAccountRequest),
                ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("AddCandidateAccounForm", dto);
        }

        [HttpGet]
        public IActionResult GetRecruterAccountCreateForm()
        {
            var recruter = Recruter();
            var user = authService.User();
            var dto = new AddRecruterAccountFormPageDTO
            {
                Recruter = recruter,
                User = user,
                ActionForSubmit = nameof(CreateRecruterAccountRequest),
                ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("AddRecruterAccountForm", dto);
        }

        [HttpGet]
        public IActionResult GetCandidateAccountEditForm(CandidateAccountDTO account)
        {
            var candidate = Candidate();
            var user = authService.User();
            var dto = new EditCandidateAccountFormPageDTO
            {
                Records = account,
                Candidate = candidate,
                User = user,
                ActionForSubmit = nameof(EditCandidateAccountRequest),
                ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("EditCandidateAccountForm", dto);
        }

        [HttpGet]
        public IActionResult GetRecruterAccountEditForm(RecruterAccountDTO account)
        {
            var recruter = Recruter();
            var user = authService.User();
            var dto = new EditRecruterAccountFormPageDTO
            {
                Records = account,
                Recruter = recruter,
                User = user,
                ActionForSubmit = nameof(EditRecruterAccountRequest),
                ControllerForSubmit = ControllerContext.ActionDescriptor.ControllerName
            };
            return View("EditRecruterAccountForm", dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidateAccountRequest(AddCandidateAccountDTO createRecordsDTO)
        {
            return await ValidationDecoratorAsync(async () =>
            {
                await salesforceService.CreateCandidateAccountAsync(createRecordsDTO);
                alertService.RaiseSuccess();
                return RedirectToAction(nameof(AccountForm));
            }, nameof(AccountForm));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecruterAccountRequest(AddRecruterAccountDTO createRecordsDTO)
        {
            return await ValidationDecoratorAsync(async () =>
            {
                await salesforceService.CreateRecruterAccountAsync(createRecordsDTO);
                alertService.RaiseSuccess();
                return RedirectToAction(nameof(AccountForm));
            }, nameof(AccountForm));
        }

        [HttpPost]
        public async Task<IActionResult> EditCandidateAccountRequest(EditCandidateAccountDTO editRecordsDTO)
        {
            return await ValidationDecoratorAsync(async () =>
            {
                await salesforceService.EditCandidateAccountAsync(editRecordsDTO);
                alertService.RaiseSuccess();
                return RedirectToAction(nameof(AccountForm));
            }, nameof(AccountForm));
        }

        [HttpPost]
        public async Task<IActionResult> EditRecruterAccountRequest(EditRecruterAccountDTO editRecordsDTO)
        {
            return await ValidationDecoratorAsync(async () =>
            {
                await salesforceService.EditRecruterAccountAsync(editRecordsDTO);
                alertService.RaiseSuccess();
                return RedirectToAction(nameof(AccountForm));
            }, nameof(AccountForm));
        }
    }
}
