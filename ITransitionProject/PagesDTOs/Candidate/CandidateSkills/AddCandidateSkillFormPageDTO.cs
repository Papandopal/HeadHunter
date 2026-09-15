using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkills
{
    public class AddCandidateSkillFormPageDTO
    {
        public required Skill Skill { get; set; }
        public Dictionary<string, string> EventsHandlers { get; set; } = new();
        public string ControllerForSubmit { get; set; } = string.Empty;
        public string ActionForSubmit {  get; set; } = string.Empty;
        public int CountOfRequiredProperties { get; set; }

    }
}
