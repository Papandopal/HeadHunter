using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.Positions
{
    public class EditPositionAccessRulePagePartialFormDTO
    {
        public required AccessRule AccessRule { get; set; }
        public Dictionary<string, string> EventHandlers { get; set; } = new();
    }
}
