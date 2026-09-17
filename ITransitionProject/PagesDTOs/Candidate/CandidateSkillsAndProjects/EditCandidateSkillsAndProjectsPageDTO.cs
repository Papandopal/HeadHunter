using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.DTOs;

namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkillsAndProjects
{
    public class EditCandidateSkillsAndProjectsPageDTO
    {
        public EditCandidateSkillsAndProjectsDTO? PostedModel { get; } = null;  
        public required IEnumerable<CandidateSkill> CandidateSkills { get; set; }
        public required IEnumerable<Project> Projects { get; set; }
        public Dictionary<string, string> EventHandlers { get; set; } = new();
        public required string ActionForGetProjectTags { get; set; } = string.Empty;
        public required string ControllerForGetProjectTags { get; set; } = string.Empty;
        public required string ActionForGetSkillsNames { get; set; } = string.Empty;
        public required string ControllerForGetSkillsNames { get; set; } = string.Empty;
        public required string ActionForGetSkillForm { get; set; } = string.Empty;
        public required string ControllerForGetSkillForm { get; set; } = string.Empty;
        public required string ActionForGetProjectForm { get; set; } = string.Empty;
        public required string ControllerForGetProjectForm { get; set; } = string.Empty;
        public required string ActionToSubmit { get; set; } = string.Empty;
        public required string ControllerToSubmit { get; set; } = string.Empty;
    }
}
