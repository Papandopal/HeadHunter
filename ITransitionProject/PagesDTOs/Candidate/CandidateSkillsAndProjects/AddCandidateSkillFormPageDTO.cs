using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkillsAndProjects
{
    public class AddCandidateSkillFormPageDTO
    {
        public required Skill Skill { get; set; }
        public Dictionary<string, string> EventsHandlers { get; set; } = new();
    }
}
