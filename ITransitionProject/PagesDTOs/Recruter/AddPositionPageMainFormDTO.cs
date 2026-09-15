using UseCases.Services.PositionServices.DTOs;

namespace ITransitionProject.PagesDTOs.Recruter
{
    public class AddPositionPageMainFormDTO
    {
        public AddPositionDTO PostedModel { get; } = new();
        public string ActionForSubmit { get; set; } = string.Empty;
        public string ControllerForSubmit { get; set; } = string.Empty;
        public string ActionForGetSkillsNames { get; set; } = string.Empty;
        public string ControllerForGetSkillsNames { get; set; } = string.Empty;
        public string ActionForGetSkillForm { get; set; } = string.Empty;
        public string ControllerForGetSkillForm { get; set; } = string.Empty;
        public string ActionForGetAccessRuleForm {  get; set; } = string.Empty;
        public string ControllerForGetAccessRuleForm { get; set; } = string.Empty;
        public string ActionForGetProjectTags {  get; set; } = string.Empty;    
        public string ControllerForGetProjectTags { get; set; } = string.Empty;
        public string ActionForGetSkillTypeBySkillName {  get; set; } = string.Empty;
        public string ControllerForGetSkillTypeBySkillName { get; set; } = string.Empty;
        public string ActionForGetSkillIdBySkillName { get; set; } = string.Empty;
        public string ControllerForGetSkillIdBySkillName { get; set; } = string.Empty;
    }
}
