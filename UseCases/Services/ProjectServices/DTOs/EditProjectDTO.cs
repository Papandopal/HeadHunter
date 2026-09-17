using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace UseCases.Services.ProjectServices.DTOs
{
    public class EditProjectDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public string? DatePeriod { get; set; } 
        public IEnumerable<ProjectTag>? ProjectTags { get; set; }
    }
}
