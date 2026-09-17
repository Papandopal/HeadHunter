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
        [JsonPropertyName("ProjectIndex")]
        public string ProjectIndex { get; set; }
        [JsonPropertyName("PropName")]
        public string PropName { get; set; } = string.Empty;
        [JsonPropertyName("PropValue")]
        public string PropValue { get; set; } = string.Empty;
    }
}
