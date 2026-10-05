using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace UseCases.Services.SalesforceServices.DTOs.Candidate
{
    public class CandidateAccountDTO
    {
        public required string AccountId { get; set; }
        public required string ContactId { get; set; }
        public required Gender Gender { get; set; }
        public required string Phone { get; set; }
    }
}
