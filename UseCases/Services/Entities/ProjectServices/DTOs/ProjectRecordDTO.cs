using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UseCases.Services.ProjectServices.DTOs
{
    public class ProjectRecordDTO
    {
        [JsonPropertyName(nameof(ProjectIndex))]
        public string ProjectIndex { get; set; }
        [JsonPropertyName(nameof(PropName))]
        public string PropName { get; set; } = string.Empty;
        [JsonPropertyName(nameof(PropValue))]
        public string PropValue { get; set; } = string.Empty;
    }
}
