using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkills
{
    public class AddCandidateSkillSelectTypePageDTO
    {
        public string ControllerForGetSkillsNames { get; set; } = string.Empty;
        public string ActionForGetSkillsNames { get; set; } = string.Empty;
        public string ControllerForGetForm { get; set; } = string.Empty;
        public string ActionForGetForm { get; set;} = string.Empty;
    }
}
