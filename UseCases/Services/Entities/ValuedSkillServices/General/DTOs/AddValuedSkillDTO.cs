using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Enums;

namespace UseCases.Services.ValuedSkillServices.General.DTOs
{
    public class AddValuedSkillDTO
    {
        [JsonPropertyName("SkillId")]
        public Guid SkillId { get; set; }
        [JsonPropertyName("Value")]
        public string Value { get; set; } = string.Empty;
    }
}
