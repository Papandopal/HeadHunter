using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UseCases.Services.CandidateServices.Interfaces;
using UseCases.Services.RecruterServices.Interfaces;
using UseCases.Services;
using Domain.Enums;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.AuthServices.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ITransitionProject.Controllers
{
    public class AuthFromExternServiceController(IAuthService authService,
        AlertService alertService) : Controller
    {
        [HttpPost]
        public IActionResult GoogleAuthorizeForm()
        {
            var redirectUrl = Url.Action("AuthorizeFromGoogle", ControllerContext.ActionDescriptor.ControllerName);
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        private bool IsAuthResultValid(AuthenticateResult result)
        {
            if (!result.Succeeded || result.Principal == null)
            {
                alertService.RaiseAlert(result.Failure?.Message ?? "Authorization with Google failed", AlertTypes.Danger);
                return false;
            }
            return true;
        }

        private bool IsEmailValid(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                alertService.RaiseAlert("Email in authentication result not found", AlertTypes.Danger);
                return false;
            }
            return true;
        }

        private bool IsUserValid(AuthorizedUserDTO? user)
        {
            if (user is null)
            {
                alertService.RaiseAlert("User not found", AlertTypes.Danger);
                return false;
            }
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> AuthorizeFromGoogle()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if(!IsAuthResultValid(authenticateResult)) return RedirectToAction("Login", "Auth"); 
            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            if(!IsEmailValid(email)) return RedirectToAction("Login", "Auth");
            var dto = new VerifyUserFromGoogleDTO { Email = email! };
            var user = await authService.TryAuthorizeFromGoogle(dto);
            if (!IsUserValid(user)) return RedirectToAction("Login", "Auth");
            return RedirectToAction("Home", user.Role.Name);    
        }
    }
}
