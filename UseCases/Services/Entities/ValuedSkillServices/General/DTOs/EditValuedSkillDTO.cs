using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UseCases.Services.ValuedSkillServices.General.DTOs
{
    public record EditValuedSkillDTO
    {
        [JsonPropertyName(nameof(ValuedSkillId))]
        public Guid ValuedSkillId { get; set; }

        [JsonPropertyName(nameof(Value))]
        public string Value { get; set; } = string.Empty;

        [JsonPropertyName(nameof(Version))]
        public long Version { get; set; }
    }
}
