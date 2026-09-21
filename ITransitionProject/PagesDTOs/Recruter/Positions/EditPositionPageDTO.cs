using Domain.Entities;
using UseCases.Services.Entities.PositionServices.DTOs;

namespace ITransitionProject.PagesDTOs.Recruter.Positions
{
    public class EditPositionPageDTO
    {
        public required Position Position { get; set; }
        public EditPositionDTO? PostedModel { get; } = null; 
        public required string ActionForGetPopularSkillNames { get; set; }
        public required string ControllerForGetPopularSkillNames { get; set; }
        public required string ActionForGetSkillsNames { get; set; }
        public required string ControllerForGetSkillsNames { get; set; }
        public required string ActionForGetSkillForm { get; set; }
        public required string ControllerForGetSkillForm { get; set; }
        public required string ActionForGetAddingAccessRuleForm { get; set; }
        public required string ControllerForGetAddingAccessRuleForm { get; set; }
        public required string ActionForGetProjectTags { get; set; }
        public required string ControllerForGetProjectTags { get; set; }
        public required string ActionForGetSkillTypeBySkillName { get; set; }
        public required string ControllerForGetSkillTypeBySkillName { get; set; }
        public required string ActionForGetSkillIdBySkillName { get; set; }
        public required string ControllerForGetSkillIdBySkillName { get; set; }
        public required string ActionForGetEditingAccessRuleForm { get; set; }
        public required string ControllerForGetEditingAccessRuleForm { get; set; }
        public required string ActionForSubmit { get; set; }
        public required string ControllerForSubmit { get; set; } 
    }
}
