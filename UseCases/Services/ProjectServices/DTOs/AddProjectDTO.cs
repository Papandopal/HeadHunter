using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Services.ProjectServices.DTOs
{
    public class AddProjectDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DatePeriod { get; set; } = string.Empty;
        public IEnumerable<ProjectTag> ProjectTags { get; set; }
    }
}
