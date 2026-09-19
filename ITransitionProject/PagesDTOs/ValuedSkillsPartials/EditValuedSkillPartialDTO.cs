using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Services.ValuedSkillServices.General.DTOs;

namespace ITransitionProject.PagesDTOs.CandidateSkillsPartials
{
    public class EditValuedSkillPartialDTO
    {
        public ValuedSkillDTO ValuedSkill { get; set; }
        public Dictionary<string, string> EventHandlers { get; set; } = new();
    }
}
