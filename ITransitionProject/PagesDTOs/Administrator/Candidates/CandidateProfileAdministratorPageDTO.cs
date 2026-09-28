using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Administrator.Candidates
{
    public class CandidateProfileAdministratorPageDTO
    {
        public Guid CandidateId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; } 
        public required DateOnly BirthDay { get; set; }
        public required IEnumerable<CandidateSkill> CandidateSkills { get; set; }
        public required IEnumerable<Project> Projects { get; set; }
        public required string ActionForEditProfile { get; set; }
        public required string ControllerForEditProfile { get; set; }
    }
}
