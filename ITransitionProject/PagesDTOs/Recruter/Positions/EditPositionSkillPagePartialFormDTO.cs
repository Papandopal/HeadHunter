using Domain.Entities;

namespace ITransitionProject.PagesDTOs.Recruter.Positions
{
    public class EditPositionSkillPagePartialFormDTO
    {
        public Skill Skill { get; set; }
        public Dictionary<string, string> EventsHandlers { get; set; } = new();
    }
}
