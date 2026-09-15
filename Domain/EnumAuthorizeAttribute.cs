using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Domain
{
    public class EnumAuthorizeAttribute : AuthorizeAttribute
    {
        public EnumAuthorizeAttribute(params UserRoles[] userRole) 
        {
            Roles = string.Join(",", userRole);
        }
    }
}
