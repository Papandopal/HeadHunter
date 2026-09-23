using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Enums;

namespace UseCases.Services.ValuedSkillServices.General.DTOs
{
    public class AddValuedSkillDTO
    {
        [JsonPropertyName(nameof(SkillId))]
        public Guid SkillId { get; set; }
        [JsonPropertyName(nameof(Value))]
        public string Value { get; set; } = string.Empty;
    }
}
