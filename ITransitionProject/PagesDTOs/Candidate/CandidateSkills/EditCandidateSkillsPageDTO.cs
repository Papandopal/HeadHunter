using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkills
{
    public class EditCandidateSkillsPageDTO
    {
        public required IEnumerable<CandidateSkill> CandidateSkills { get; set; }
        public Dictionary<string, string> EventHandlers { get; set; } = new();
        public string ControllerToSubmit { get; set; } = string.Empty;
        public string ActionToSubmit { get; set; } = string.Empty;
        public int CountOfRequiredProperties { get; set; }
    }
}
