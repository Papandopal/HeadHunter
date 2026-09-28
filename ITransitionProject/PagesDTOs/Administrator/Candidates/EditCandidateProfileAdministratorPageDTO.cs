using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITransitionProject.PagesDTOs.Administrator.Candidates
{
    public class EditCandidateProfileAdministratorPageDTO
    {
        public Guid CandidateId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; } 
        public required DateOnly BirthDay { get; set; }
        public required string ActionForSubmit { get; set; }
        public required string ControllerForSubmit { get; set; }
    }
}
