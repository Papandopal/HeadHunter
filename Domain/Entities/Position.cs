using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Position
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<PositionSkill> PositionSkills { get; set; } = new List<PositionSkill>();
        public IEnumerable<AccessRule> AccessRules { get; set; } = new List<AccessRule>();
        public IEnumerable<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();
        public int MaxCountOfProject { get; set; }
    }
}
