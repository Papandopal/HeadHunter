using Domain.Entities;

namespace ITransitionProject.PagesDTOs.AccessRules
{
    public class EditAccessRulePartialDTO
    {
        public AccessRule AccessRule { get; set; }
        public Dictionary<string, string> EventHandlers { get; set; } = new();
    }
}
