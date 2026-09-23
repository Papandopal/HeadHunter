using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Services.AuthServices.DTOs;

namespace UseCases.Services.AuthServices.Interfaces
{
    public interface IAuthService
    {
        public AuthorizedUserDTO User();
        public Task<AuthorizedUserDTO?> Verify(VerifyUserDTO verifyUserDTO);
        public Task<AuthorizedUserDTO?> Registrate(RegistrateUserDTO registrateUserDTO);
        public Task<AuthorizedUserDTO?> TryAuthorizeFromGoogle(VerifyUserFromGoogleDTO verifyUserFromGoogleDTO);
        public void Validate();
    }
}
