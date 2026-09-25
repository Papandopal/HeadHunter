using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Skill
    {
        public Guid Id { get; init; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PotentialValue { get; set; } = string.Empty;
        public SkillTypes Type { get; set; }
        public long Popularity { get; set; }    
        public long Version { get; set; } = 0;
    }
}
