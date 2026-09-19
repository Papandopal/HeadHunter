using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.ValuedSkillServices.CandidateSkillServices.DTOs
{
    public class EditCandidateSkillsAndProjectsDTO
    {
        public string BufferForUpdatingSkills {  get; set; } = string.Empty;
        public string BufferForAddedSkills { get; set; } = string.Empty;
        public string BufferForUpdatingProjects{ get; set; } = string.Empty;
        public string BufferForAddedProjects { get; set; } = string.Empty;
    }
}
