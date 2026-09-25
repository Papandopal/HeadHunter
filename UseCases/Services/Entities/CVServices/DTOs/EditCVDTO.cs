using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Services.Entities.CVServices.DTOs
{
    public class EditCVDTO
    {
        public required Guid OwnerId { get; set; }
        public long Version { get; set; }   
        public string BufferForUpdatingSkills { get; set; } = string.Empty;
        public string BufferForUpdatingProjects { get; set; } = string.Empty;
    }
}
