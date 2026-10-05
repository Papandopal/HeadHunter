using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace UseCases.Services.SalesforceServices.DTOs.Recruter
{
    public class AddRecruterAccountDTO
    {
        public DateOnly Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}
