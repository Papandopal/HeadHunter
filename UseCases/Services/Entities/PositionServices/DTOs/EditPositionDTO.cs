using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.Entities.PositionServices.DTOs
{
    public class EditPositionDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BufferForSkills { get; set; } = string.Empty;
        public string BufferForAccessRules { get; set; } = string.Empty;
        public string BufferForProjectTags { get; set; } = string.Empty;
        public int MaxCountOfProject { get; set; }
        public long Version { get; set; }   
    }
}
