using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.Positions
{
    public class AddPositionAccessRulePagePartialFormDTO
    {
        public Skill Skill { get; set; }
        public Dictionary<string, string> EventHandlers { get; set; } = new();
    }
}
