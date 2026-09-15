using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.SkillServices.DTOs
{
    public class UpdateSkillDTO
    {
        public Guid SkillId { get; set; }
        public string PropName { get; set; } = string.Empty;
        public string PropValue { get; set; } = string.Empty;
    }
}
