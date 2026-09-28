using System.Security.Claims;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using UseCases.Database;
using UseCases.Services.Exceptions;

namespace UseCases.Services.AuthServices
{
    public class AccessValidator(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        public bool Validate()
        {
            var context = httpContextAccessor.HttpContext;
            if (context is null) throw new FailedAuthValidationException("Current http context is null");
            var user_email = context.User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.Email)?.Value;
            if (user_email is null) throw new FailedAuthValidationException("Email of current user not found");
            var user = unitOfWork.UserRepository.FirstOrDefaultByEmail(user_email);
            if (user is null) throw new FailedAuthValidationException("User not found");
            return user.Status != UserStatus.Blocked;
        }
    }
}
