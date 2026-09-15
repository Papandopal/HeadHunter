using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Services.ValuedSkillServices.GeneralDTOs
{
    public class ValuedSkillDTO
    {
        public Guid Id { get; set; }
        public Skill Skill { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
