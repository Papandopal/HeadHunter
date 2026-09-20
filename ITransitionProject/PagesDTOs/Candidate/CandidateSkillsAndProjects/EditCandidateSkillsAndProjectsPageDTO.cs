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
        public required string ActionForGetProjectTags { get; set; }
        public required string ControllerForGetProjectTags { get; set; }
        public required string ActionForGetSkillsNames { get; set; }
        public required string ControllerForGetSkillsNames { get; set; }
        public required string ActionForGetSkillForm { get; set; }
        public required string ControllerForGetSkillForm { get; set; }
        public required string ActionForGetProjectForm { get; set; }
        public required string ControllerForGetProjectForm { get; set; }
        public required string ActionForUploadImage { get; set; }
        public required string ControllerForUploadImage { get; set; }
        public required string ActionForGetPopularSkillNames { get; set; }
        public required string ControllerForGetPopularSkillNames { get; set; }
        public required string ActionToSubmit { get; set; }
        public required string ControllerToSubmit { get; set; }
    }
}
