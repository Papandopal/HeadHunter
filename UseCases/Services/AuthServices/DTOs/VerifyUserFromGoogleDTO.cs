using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.AuthServices.DTOs
{
    public class VerifyUserFromGoogleDTO
    {
        public required string Email { get; set; }
    }
}
