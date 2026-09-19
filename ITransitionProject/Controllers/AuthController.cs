using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Domain.Enums;
using UseCases.Services.AuthServices.DTOs;
using UseCases.Services;
using UseCases.Services.AuthServices.Interfaces;

namespace ITransitionProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly AlertService _alertService;

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration, IAuthService authService,
            AlertService alertService)
        {
            _logger = logger;
            _configuration = configuration;
            _authService = authService;
            _alertService = alertService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            AuthorizedUserDTO user;
            try
            {
                user = _authService.User();
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Home", $"{user.Role.Name}");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(_configuration["AuthCookieName"]!);
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return RedirectToAction("Login");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> RemoteLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(_configuration["AuthCookieName"]!);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VerifyUserDTO verifyUserDTO)
        {
            AuthorizedUserDTO? user = await _authService.Verify(verifyUserDTO);
            if (user is null)
            {
                _alertService.RaiseAlert("User not found", AlertTypes.Warning);
                return View();
            }
            return RedirectToAction("Home", user.Role.Name);
        }


        [HttpPost]
        public async Task<IActionResult> Registration(RegistrateUserDTO registrateUserDTO)
        {
            AuthorizedUserDTO? user = await _authService.Registrate(registrateUserDTO);
            if (user is null)
            {
                _alertService.RaiseAlert("Cant registrate user", AlertTypes.Danger);
                return View();
            }
            return RedirectToAction("Home", user.Role.Name);
        }
    }
}
