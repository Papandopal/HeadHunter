using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Services.SalesforceServices.DTOs.Candidate;
using UseCases.Services.SalesforceServices.DTOs.Recruter;

namespace UseCases.Services.SalesforceServices.Interfaces
{
    public interface ISalesforceService
    {
        public Task LoginAsync();
        public Task CreateCandidateAccountAsync(AddCandidateAccountDTO addRecordsDTO);
        public Task EditCandidateAccountAsync(EditCandidateAccountDTO editRecordsDTO);
        public Task CreateRecruterAccountAsync(AddRecruterAccountDTO addRecordsDTO);
        public Task EditRecruterAccountAsync(EditRecruterAccountDTO editRecordsDTO);
        public Task<CandidateAccountDTO?> TryGetCandidateAccountAsync();
        public Task<RecruterAccountDTO?> TryGetRecruterAccountAsync();
    }
}
