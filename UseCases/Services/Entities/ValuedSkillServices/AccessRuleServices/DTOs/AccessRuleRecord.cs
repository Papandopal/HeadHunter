using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace UseCases.Services.Entities.ValuedSkillServices.AccessRuleServices.DTOs
{
    public class AccessRuleRecord
    {
        public Guid SkillId { get; set; }
        public string PropName { get; set; } = string.Empty;
        public string PropValue { get; set; } = string.Empty;
    }
}
