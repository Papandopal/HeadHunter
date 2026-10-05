using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace UseCases.Services.SalesforceServices.DTOs.Candidate
{
    public class AddCandidateAccountDTO
    {
        public Gender Gender { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}
