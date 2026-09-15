using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PositionSkill
    {
        public Guid Id { get; set; }
        public Guid? PositionId { get; set; }
        public Position? Position { get; set; }
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
