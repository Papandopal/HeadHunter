using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UseCases.Services.ValuedSkillServices.GeneralDTOs
{
    public record EditValuedSkillDTO
    {
        [JsonPropertyName("ValuedSkillId")]
        public Guid ValuedSkillId { get; set; }
        [JsonPropertyName("Value")]
        public string Value { get; set; } = string.Empty;
    }
}
