using System.Security.Claims;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using UseCases.Database;
using UseCases.Services.AuthServices.DTOs;
using UseCases.Services.AuthServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace UseCases.Services.AuthServices
{
    public class AuthService(IUnitOfWork unitOfWork, CryptService cryptService, IMapper mapper, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor, AccessValidator accessValidator) : IAuthService
    {
        AuthorizedUserDTO IAuthService.User()
        {
            if (!httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? true) throw new Exception("User not authorized");
            var email = httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            if (email is null) throw new Exception("email not found in user claimss");
            var user = unitOfWork.UserRepository.FirstOrDefaultByEmail(email.Value);
            if (user is null) throw new Exception("user not found");
            return mapper.Map<AuthorizedUserDTO>(user);
        }
        private async Task SetCookies(AuthorizedUserDTO user, string roleName, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Status", user.Status.ToString()),
                new Claim(ClaimTypes.Role, roleName)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            AuthenticationProperties? authProperties = null;
            if (!rememberMe) authProperties = new AuthenticationProperties { IsPersistent = false };
            configuration["RememberMe"] = rememberMe.ToString();

            await (httpContextAccessor.HttpContext?.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
            authProperties) ?? Task.CompletedTask);
        }
        async Task<AuthorizedUserDTO?> IAuthService.Registrate(RegistrateUserDTO registrateUserDTO)
        {
            var user = mapper.Map<User>(registrateUserDTO);
            user.PasswordHash = cryptService.CreatePasswordHash(registrateUserDTO.Password);
            user.Role = registrateUserDTO.Role;
            try
            {
                unitOfWork.StartTransaction();
                unitOfWork.UserRepository.Add(user);
                //if (user.Role == UserRole.Candidate.ToString())
                //{
                //    unitOfWork.CandidateRepository.AddByUser(user);
                //    var accountId = unitOfWork.CandidateRepository.GetByOwnerId(user.Id).Id;
                //    user.AccountId = accountId;
                //    unitOfWork.UserRepository.Update(user);
                //}
                unitOfWork.Commit();
                var authorizedUser = mapper.Map<AuthorizedUserDTO>(user);
                await SetCookies(authorizedUser, registrateUserDTO.Role, registrateUserDTO.RememberMe);
                return authorizedUser;
            }
            catch
            {
                await unitOfWork.RollbackAsync();
                return null;
            }
        }

        async Task<AuthorizedUserDTO?> IAuthService.Verify(VerifyUserDTO verifyUserDTO)
        {
            var user = unitOfWork.UserRepository.FirstOrDefaultByEmail(verifyUserDTO.Email);
            if (user is null || !cryptService.VerifyPassword(verifyUserDTO.Password, user.PasswordHash)) return null;
            var roleName = user.Role;
            var authorizedUser = mapper.Map<AuthorizedUserDTO>(user);
            await SetCookies(authorizedUser, roleName!, verifyUserDTO.RememberMe);
            return authorizedUser;
        }

        void IAuthService.Validate()
        {
            accessValidator.Validate();
        }
    }
}
