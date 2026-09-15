using Domain.Entities;

namespace ITransitionProject.PagesDTOs.AccessRules
{
    public class AddAccessRulePartialDTO
    {
        public Skill Skill { get; set; }
        public Dictionary<string, string> EventHandlers { get; set; } = new();
    }
}
