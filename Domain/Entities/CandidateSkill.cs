using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public record CandidateSkill
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }
        public string Value { get; set; } = string.Empty;
        public long Version { get; set; } = 0;
    }
}
