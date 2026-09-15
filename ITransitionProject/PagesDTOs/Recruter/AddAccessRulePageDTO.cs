using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter
{
    public class AddAccessRulePageDTO
    {
        public Skill Skill { get; set; }
        public Dictionary<string, string> EventHandlers { get; set; } = new();
    }
}
