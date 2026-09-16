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
        public string ControllerForGetSkillsNames { get; set; } = string.Empty;
        public string ActionForGetSkillsNames { get; set; } = string.Empty;
        public string ControllerForGetSkillForm { get; set; } = string.Empty;
        public string ActionForGetSkillForm { get; set;} = string.Empty;
        public string ControllerForGetProjectForm {  get; set; } = string.Empty;
        public string ActionForGetProjectForm { get;set; } = string.Empty;
        public string ControllerForSubmit { get; set; } = string.Empty;
        public string ActionForSubmit { get; set; } = string.Empty;
    }
}
