using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class AccessRule
    {
        public Guid Id { get; set; }
        public Guid? PositionId { get; set; }
        public Position? Position { get; set; }
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }
        public FilterOperators Operator { get; set; }
        public string Value { get; set; } = string.Empty;
        public long Version { get; set; } = 0;
    }
}
