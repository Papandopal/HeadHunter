using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace ITransitionProject.PagesDTOs.CandidateSkillsPartials
{
    public class AddValuedSkillPartialDTO
    {
        public Skill Skill { get; set; }
        public Dictionary<string, string> EventHandlers { get; set; } = new();
    }
}
