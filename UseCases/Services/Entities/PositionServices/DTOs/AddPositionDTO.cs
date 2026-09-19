using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.PositionServices.DTOs
{
    public class AddPositionDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BufferForSkills { get; set; } = string.Empty;
        public string BufferForAccessRules {  get; set; } = string.Empty;
        public string BufferForProjectTags {  get; set; } = string.Empty;
        public int MaxCountOfProject {  get; set; }
    }
}
