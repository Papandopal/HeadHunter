using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace UseCases.Services.AuthServices.DTOs
{
    public class AuthorizedUserDTO
    {
        public Guid Id { get; init; }
        public string Email { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = false;
        public required IdentityRole Role { get; init; }
        public UserStatus Status { get; set; }
    }
}
