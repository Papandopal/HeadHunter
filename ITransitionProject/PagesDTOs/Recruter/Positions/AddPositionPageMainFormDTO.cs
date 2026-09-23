using UseCases.Services.PositionServices.DTOs;

namespace ITransitionProject.PagesDTOs.Recruter.Positions
{
    public class AddPositionPageMainFormDTO
    {
        public AddPositionDTO PostedModel { get; } = new();
        public required string ActionForGetPopularSkillNames {  get; set; } 
        public required string ControllerForGetPopularSkillNames { get; set; } 
        public string ActionForGetSkillsNames { get; set; } = string.Empty;
        public string ControllerForGetSkillsNames { get; set; } = string.Empty;
        public string ActionForGetSkillForm { get; set; } = string.Empty;
        public string ControllerForGetSkillForm { get; set; } = string.Empty;
        public string ActionForGetAddingAccessRuleForm {  get; set; } = string.Empty;
        public string ControllerForGetAddingAccessRuleForm { get; set; } = string.Empty;
        public string ActionForGetProjectTags {  get; set; } = string.Empty;    
        public string ControllerForGetProjectTags { get; set; } = string.Empty;
        public required string ActionForGetPopularProjectTags {  get; set; }
        public required string ControllerForGetPopularProjectTags { get; set; }
        public string ActionForGetSkillTypeBySkillName {  get; set; } = string.Empty;
        public string ControllerForGetSkillTypeBySkillName { get; set; } = string.Empty;
        public string ActionForGetSkillIdBySkillName { get; set; } = string.Empty;
        public string ControllerForGetSkillIdBySkillName { get; set; } = string.Empty;
        public string ActionForSubmit { get; set; } = string.Empty;
        public string ControllerForSubmit { get; set; } = string.Empty;
    }
}
