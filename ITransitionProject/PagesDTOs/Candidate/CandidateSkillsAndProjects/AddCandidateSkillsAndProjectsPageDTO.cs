using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.DTOs;

namespace ITransitionProject.PagesDTOs.Candidate.CandidateSkillsAndProjects
{
    public class AddCandidateSkillsAndProjectsPageDTO
    {
        public AddCandidateSkillsAndProjectsDTO? PostedModel { get; } = null;
        public required string ControllerForGetSkillsNames { get; set; } = string.Empty;
        public required string ActionForGetSkillsNames { get; set; } = string.Empty;
        public required string ControllerForGetSkillForm { get; set; } = string.Empty;
        public required string ActionForGetSkillForm { get; set;} = string.Empty;
        public required string ControllerForGetProjectForm {  get; set; } = string.Empty;
        public required string ActionForGetProjectForm { get;set; } = string.Empty;
        public required string ControllerForSubmit { get; set; } = string.Empty;
        public required string ActionForSubmit { get; set; } = string.Empty;
    }
}
